using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CensusApp.Api.Core.Domain.Extensions
{
    public static class FluntExtensions
    {
        public static string ToListString(this IReadOnlyCollection<Notification> notifications)
        {
            var strNotifications = new StringBuilder();

            foreach (var item in notifications)
            {
                strNotifications.Append(item.Message);
                strNotifications.Append(Environment.NewLine);
            }

            return strNotifications.ToString();
        }
        public static bool HasNotification(this Notifiable<Notification> notifiable, string key = null)
        {
            if (key is not null)
            {
                return notifiable.Notifications.Any(x => x.Key == key);
            }

            return notifiable.Notifications.Any();
        }
        public static bool CountNotificationEquals(this Notifiable<Notification> notifiable, int value)
        {
            return notifiable.Notifications.Count.Equals(value);
        }
        public static bool HasNotificationMessage(this Notifiable<Notification> notifiable, string message)
        {
            return notifiable.Notifications.Any(x => x.Message == message);
        }
        public static Contract<Notification> IsNotNullOrEmptyWithDefaultMessage(this Contract<Notification> contract,string prop, string val)
        {
            contract.IsNotNullOrEmpty(val, $"{prop.ToLower()}_is_not_null", $"O valor atribuído para {prop} não pode ser nulo");

            return contract;
        }
        public static Contract<Notification> IsNotNullWithDefaultMessage(this Contract<Notification> contract,string prop, object val)
        {
            contract.IsNotNull(val, $"{prop.ToLower()}_is_not_null", $"O valor atribuído para {prop} não pode ser nulo");

            return contract;
        }

    }
}
