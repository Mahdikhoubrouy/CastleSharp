using CastleSharp.Core.Attributes;
using CastleSharp.Core.Exceptions;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CastleSharp.Core
{
    public class TelegramCastleSharp
    {
        #region Fields

        private Assembly _assembly;
        private ITelegramBotClient _telegramBotClient;

        #endregion

        #region Init

        public TelegramCastleSharp Configure(ITelegramBotClient botclient, Assembly assembly)
        {
            _assembly = assembly;
            _telegramBotClient = botclient;
            return this;
        }

        #endregion

        #region Helper Methods

        private List<MethodInfo> FindMethodByType<Tmethod>()
        {
            var methods = _assembly.GetTypes()
                           .SelectMany(t => t.GetMethods())
                           .Where(m => m.GetCustomAttributes(typeof(Tmethod), false).Length > 0)
                           .ToList();
            return methods;
        }

        private List<MethodInfo> FindConditionByName(string name)
        {
            var conditions = _assembly.GetTypes()
                           .SelectMany(t => t.GetMethods())
                           .Where(m => m.Name == name)
                           .ToList();
            return conditions;
        }

        private void InvokeMethod<TInvokeData>(MethodInfo method, Attribute attribute, ITelegramBotClient botClient, TInvokeData invokeData)
        {
            method.Invoke(attribute, [botClient, invokeData]);
        }

        #endregion

        #region Command

        public Task<CastleResponse> HandleCommandAsync(Message message)
        {
            var methods = FindMethodByType<CommandAttribute>();

            foreach (var method in methods)
            {
                if (method is null)
                    continue;

                var attribute = method.GetCustomAttribute(typeof(CommandAttribute), false);

                if (attribute is null)
                    continue;

                var methodType = attribute.GetType();
                object? value = methodType.GetProperty("CommandText")!.GetValue(attribute);
                object? conditionName = methodType.GetProperty("ConditionName")!.GetValue(attribute);

                if (!string.IsNullOrWhiteSpace(conditionName?.ToString()))
                {
                    var condtion = FindConditionByName(conditionName!.ToString()!);

                    if (condtion.Count > 1)
                        throw new CustomConditionException("The number of found conditions is more than one");
                    if (condtion.Count < 1)
                        throw new NotFoundCustomConditionException();

                    var customCondition = condtion.First();
                    var result = (bool)customCondition.Invoke(customCondition, [message])!;

                    if (result)
                    {
                        InvokeMethod(method, attribute, _telegramBotClient, message);
                        return Task.FromResult(CastleResponse.Success());
                    }
                }

                if (value is null)
                    continue;

                if (value.ToString() != message.Text)
                    continue;

                InvokeMethod(method, attribute, _telegramBotClient, message);
                return Task.FromResult(CastleResponse.Success());
            }

            return Task.FromResult(CastleResponse.Failed());
        }

        #endregion

        #region Callback Query

        public Task<CastleResponse> HandleCallBackQueryAsync(CallbackQuery callback)
        {
            var methods = FindMethodByType<CallBackQueryAttribute>();

            foreach (var method in methods)
            {
                if (method is null)
                    continue;

                var attribute = method.GetCustomAttribute(typeof(CallBackQueryAttribute), false);

                if (attribute is null)
                    continue;

                var methodType = attribute.GetType();
                object? value = methodType.GetProperty("CallBackQueryText")!.GetValue(attribute);
                object? conditionName = methodType.GetProperty("ConditionName")!.GetValue(attribute);

                if (!string.IsNullOrWhiteSpace(conditionName?.ToString()))
                {
                    var condtion = FindConditionByName(conditionName!.ToString()!);

                    if (condtion.Count > 1)
                        throw new CustomConditionException("The number of found conditions is more than one");
                    if (condtion.Count < 1)
                        throw new NotFoundCustomConditionException();

                    var customCondition = condtion.First();
                    var result = (bool)customCondition.Invoke(customCondition, [callback])!;

                    if (result)
                    {
                        InvokeMethod(method, attribute, _telegramBotClient, callback);
                        return Task.FromResult(CastleResponse.Success());
                    }
                }

                if (value is null)
                    continue;

                if (value.ToString() != callback.Data)
                    continue;

                InvokeMethod(method, attribute, _telegramBotClient, callback);
                return Task.FromResult(CastleResponse.Success());
            }

            return Task.FromResult(CastleResponse.Failed());
        }

        #endregion
    }
}
