using CastleSharp.Core.Attributes;
using CastleSharp.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace CastleSharp.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class TelegramCastleSharp
    {
        #region Fields

        private Assembly _assembly;
        private ITelegramBotClient _telegramBotClient;
        private List<MethodInfo> _methods;

        #endregion

        #region Init
        /// <summary>
        /// Configures CastleSharp with the Telegram bot client and the assembly to search for command methods.
        /// </summary>
        /// <param name="botClient">The telegram bot client</param>
        /// <param name="assembly">The assembly of your project</param>
        /// <returns></returns>
        public TelegramCastleSharp Configure(ITelegramBotClient botClient, Assembly assembly)
        {
            _assembly = assembly;
            _telegramBotClient = botClient;
            _methods = GetMethods();
            return this;
        }

        #endregion

        #region Method Helpers


        private List<MethodInfo> GetMethods()
        {
            return _assembly.GetTypes()
                .SelectMany(t => t.GetMethods())
                .ToList();
        }

        private List<MethodInfo> FindConditionByName(string name)
        {
            return _methods
                    .Where(m => m.Name == name)
                        .ToList();
        }

        private void InvokeMethod<TInvokeData>(MethodInfo method, Attribute attribute, ITelegramBotClient botClient, TInvokeData invokeData)
        {
            method.Invoke(attribute, [botClient, invokeData]);
        }

        #endregion

        #region Command

        private Task<CastleResponse> HandleAsync<TAttribute>(Update update, string StaticCommandTextPropertyName, string ConditionPropertyName)
    where TAttribute : CommandBase
        {
            foreach (var method in _methods)
            {
                if (method == null) continue;

                var attribute = method.GetCustomAttribute(typeof(TAttribute), false);
                if (attribute == null) continue;

                var methodType = attribute.GetType();

                string? staticCommand = methodType.GetProperty(StaticCommandTextPropertyName)?.GetValue(attribute)?.ToString();
                string? conditionName = methodType.GetProperty(ConditionPropertyName)?.GetValue(attribute)?.ToString();

                object? passValueToMethod = update.Type switch
                {
                    UpdateType.Message => update.Message,
                    UpdateType.CallbackQuery => update.CallbackQuery,
                    _ => update
                };


                if (conditionName != null && !string.IsNullOrWhiteSpace(conditionName))
                {
                    var conditionMethods = FindConditionByName(conditionName);

                    if (conditionMethods.Count != 1)
                    {
                        throw conditionMethods.Count > 1
                            ? new CustomConditionException("Multiple conditions found with the same name.")
                            : new NotFoundCustomConditionException();
                    }

                    var condition = conditionMethods.First();
                    var result = (bool)condition.Invoke(new object(), [passValueToMethod])!;
                    if (!result) continue;

                    InvokeMethod(method, attribute, _telegramBotClient, passValueToMethod);
                    return Task.FromResult(CastleResponse.Success());
                }

                if (staticCommand == null)
                    continue;

                if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery?.Data != staticCommand)
                    continue;

                if (update.Type == UpdateType.Message && update.Message?.Text != staticCommand)
                    continue;


                InvokeMethod(method, attribute, _telegramBotClient, passValueToMethod);
                return Task.FromResult(CastleResponse.Success());
            }

            return Task.FromResult(CastleResponse.Failed());
        }


        /// <summary>
        /// Handles commands.
        /// </summary>
        /// <param name="update">The update object from telegram</param>
        /// <returns>CastleResponse</returns>
        /// <exception cref="CustomConditionException"></exception>
        /// <exception cref="NotFoundCustomConditionException"></exception>
        public Task<CastleResponse> HandleCommandsAsync(Update update)
        {
            return HandleAsync<CommandAttribute>(update, "Command", "ConditionName");
        }

        #endregion

    }

}
