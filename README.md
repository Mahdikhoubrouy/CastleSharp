
# CastleSharp Telegram Bot 🏰

CastleSharp is a C# library for building modular and scalable Telegram bots using Aspect-Oriented Programming (AOP) principles. This README provides step-by-step instructions on how to use CastleSharp, with detailed examples to get you started quickly.

## Table of Contents
1. [How to Use](#how-to-use)
2. [Usage](#usage)
3. [Command with Static Text](#command-with-static-text)
4. [Examples](#examples)
5. [Advanced Usage](#advanced-usage)
6. [Handling Callbacks and Commands](#handling-callbacks-and-commands)

## Features

- **Attribute-Based Command Handling**: CastleSharp simplifies command handling by allowing developers to define bot commands using `[Command]` attributes. This enables clear and concise command declarations.
- **Conditional Commands**: Commands can be triggered based on specific conditions, allowing bots to respond intelligently to message content, patterns, or custom logic.
- **Asynchronous Processing**: All bot commands are asynchronous, ensuring responsiveness and scalability for high-traffic bots.
- **Aspect-Oriented Programming (AOP)**: Using AOP, CastleSharp applies command attributes and conditions dynamically, creating modular, maintainable, and reusable code.
- **Scalable and Organized Structure**: CastleSharp is organized to support growth, making it easier to add features and manage complex bot logic.

## Installation

To start using CastleSharp, ensure you meet the prerequisites and follow the installation steps.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or higher is recommended)
- A bot token from [BotFather](https://core.telegram.org/bots#botfather) on Telegram
  - To create a bot token, open a conversation with [BotFather](https://core.telegram.org/bots#botfather) on Telegram, create a new bot, and note down the token provided.

### Setup Instructions

**Clone the Repository**  
   Start by cloning the CastleSharp repository to your local machine:
   ```bash
   git clone https://github.com/Mahdikhoubrouy/CastleSharp.git
   cd CastleSharp
   ```


## Getting Started

Once installation is complete, you can define and register commands to start creating your bot functionality.

### Initializing the Bot

To start your bot, create an instance of `ITelegramBotClient` with your bot token and use a bot service to manage the commands and conditions.

### Program.cs

```csharp
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using CastleSharp.Core;
using System.Reflection;

internal class Program
{
    private static ITelegramBotClient botClient = new TelegramBotClient("<TOKEN>");
    private static TelegramCastleSharp castleWindsor = new TelegramCastleSharp().Configure(botClient, Assembly.GetExecutingAssembly());

    private static async Task Main(string[] args)
    {
        using CancellationTokenSource cts = new();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMeAsync();
        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        cts.Cancel();

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;
            if (message.Text is not { } messageText)
                return;

            var response = await castleWindsor.HandleCommandAsync(message);
            if (response.IsSuccess)
                return;

            var chatId = message.Chat.Id;
            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
```

### Handler.cs
```
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CastleSharp.Core.Attributes;
using Telegram.Bot;
using Telegram.Bot.Types;
namespace CastleSharp.ConsoleTest
{
    public class Handlers
    {
        public static bool HelloCondition(Message message) => message.Text!.ToLower().StartsWith("hello");

        [Command(ConditionName = "HelloCondition")]
        public static async Task Hello(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hi ✅", replyToMessageId: message.MessageId);
        }


        [Command(CommandText = "Ping")]
        public static async Task Ping(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Pong ✅", replyToMessageId: message.MessageId);
        }
    }
}
```

## How to Use

CastleSharp uses attributes to define commands and conditions, making it easy to create interactive bot responses. Follow these steps to define commands and use CastleSharp effectively.

1. **Define a Command**: Each command is a method decorated with the `[Command]` attribute.
2. **Add Conditional Logic**: Optional conditions can be added to execute commands based on message content.
3. **Run the Bot**: Start the bot client, and CastleSharp will handle incoming messages according to your defined commands and conditions.

## Usage

CastleSharp uses attributes to define commands and manage bot behavior. Each bot command is a method decorated with the `[Command]` attribute. You can specify two main types of commands:

- **Static Command Text**: Triggered when a user sends a specific message text.
- **Conditional Commands**: Execute only when certain conditions are met, such as a specific keyword or phrase in the message.

## Command with Static Text

A static text command triggers when the bot receives an exact message. For example, a command to respond to “/start”:

```csharp
[Command(CommandText = "start")]
public static async Task StartCommand(ITelegramBotClient botClient, Message message)
{
    await botClient.SendTextMessageAsync(message.Chat.Id, "Welcome to CastleSharp Bot! 🏰", replyToMessageId: message.MessageId);
}
```

## Examples

### Basic Conditional Command

A conditional command triggers only if the message content matches a custom condition. For example, to respond to messages starting with "hello":

1. Define a condition function:
   ```csharp
   public static bool HelloCondition(Message message) => message.Text!.ToLower().StartsWith("hello");
   ```

2. Define the command with the condition:
   ```csharp
   [Command(ConditionName = "HelloCondition")]
   public static async Task HelloCommand(ITelegramBotClient botClient, Message message)
   {
       await botClient.SendTextMessageAsync(message.Chat.Id, "Hello there! 👋", replyToMessageId: message.MessageId);
   }
   ```


### Simple Ping-Pong Command

A static text command that responds to “Ping”:

```csharp
[Command(CommandText = "Ping")]
public static async Task PingCommand(ITelegramBotClient botClient, Message message)
{
    await botClient.SendTextMessageAsync(message.Chat.Id, "Pong 🏓", replyToMessageId: message.MessageId);
}
```

### Error Handling Command

To handle errors within commands, wrap the main logic in a try-catch block:

```csharp
[Command(CommandText = "errorTest")]
public static async Task ErrorTestCommand(ITelegramBotClient botClient, Message message)
{
    try
    {
        throw new Exception("Simulated error!");
    }
    catch (Exception ex)
    {
        await botClient.SendTextMessageAsync(message.Chat.Id, $"An error occurred: {ex.Message}", replyToMessageId: message.MessageId);
    }
}
```

## Advanced Usage

### Custom Conditions

Create customized commands by defining conditions. For example, to check for multiple keywords:

```csharp
public static bool MultipleKeywordsCondition(Message message)
{
    var text = message.Text!.ToLower();
    return text.Contains("castle") || text.Contains("knight");
}

[Command(ConditionName = "MultipleKeywordsCondition")]
public static async Task MultipleKeywordsCommand(ITelegramBotClient botClient, Message message)
{
    await botClient.SendTextMessageAsync(message.Chat.Id, "A message about castles or knights! 🏰⚔️", replyToMessageId: message.MessageId);
}
```

### Implementing Timed Responses

Commands with delayed responses add interactivity:

```csharp
[Command(CommandText = "wait")]
public static async Task WaitCommand(ITelegramBotClient botClient, Message message)
{
    await botClient.SendTextMessageAsync(message.Chat.Id, "Please wait... ⏳", replyToMessageId: message.MessageId);
    await Task.Delay(3000);
    await botClient.SendTextMessageAsync(message.Chat.Id, "Thank you for waiting! ✅", replyToMessageId: message.MessageId);
}
```

## Handling Callbacks and Commands

CastleSharp includes methods like `HandleCommandAsync` and `HandleCallBackQueryAsync` to process incoming messages and handle callback queries.

### HandleCommandAsync

The `HandleCommandAsync` method processes incoming messages to identify if they match any registered commands. If a command is found, it executes the method associated with that command. Use `HandleCommandAsync` to streamline message processing and focus on your bot's custom logic without manually checking each message.

### HandleCallBackQueryAsync

`HandleCallBackQueryAsync` is used to handle callback queries in response to interactive buttons or actions. This method enables you to define responses to button clicks or other inline interactions, providing a dynamic, interactive experience for users.

---

This structure gives you a strong foundation to start building feature-rich Telegram bots with CastleSharp.
