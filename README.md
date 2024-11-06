# CastleSharp Telegram Bot 🤖

CastleSharp Telegram Bot is a powerful and flexible C# library designed to simplify the development of Telegram bots. Using Aspect-Oriented Programming (AOP) principles, CastleSharp allows developers to handle bot commands through custom attributes and condition-based handling. This design promotes cleaner, more modular code and enhances reusability and maintainability for complex bot functionality.

## Table of Contents
1. [Features](#features)
2. [Installation](#installation)
3. [Getting Started](#getting-started)
4. [Usage Examples](#usage-examples)
   - [Conditional Commands](#conditional-commands)
   - [Static Command Text](#static-command-text)
5. [Architecture and Project Structure](#architecture-and-project-structure)
6. [Advanced Usage](#advanced-usage)
   - [Defining Custom Conditions](#defining-custom-conditions)
   - [Error Handling](#error-handling)
7. [Contributing](#contributing)
8. [License](#license)

## Features

- **Attribute-Based Command Handling**: Commands are defined through attributes, enabling a clean, declarative approach to creating bot functionality.
- **Aspect-Oriented Programming (AOP)**: Implements AOP principles to dynamically apply commands and conditions, promoting modular code.
- **Flexible Conditional Commands**: Define commands that respond only when specific conditions are met, such as certain keywords or message formats.
- **Asynchronous Processing**: Designed for asynchronous operations to maintain responsiveness, even under high load.
- **Simple and Modular Structure**: Organized project structure to simplify command and condition definition, making it ideal for scalable bot development.

## Installation

To get started with CastleSharp Telegram Bot, follow these installation steps.

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later is recommended)
- A bot token from [BotFather](https://core.telegram.org/bots#botfather) on Telegram

### Setup

1. **Clone the Repository**
   ```bash
   git clone https://github.com/Mahdikhoubrouy/CastleSharp.git
   cd CastleSharp
