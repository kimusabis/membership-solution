using Memberships.Entities;
using Memberships.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Memberships.Extensions
{
    public static class SubscriptionExtensions
    {
        public static async Task<int> GetSubscriptionByRegistrationCode(this IDbSet<Subscription> Subscription, string code)
        {
            try
            {
                if (Subscription == null || code == null || code.Length <= 0)
                {
                    return Int32.MinValue;
                }

                var subscriptionId = await (from s in Subscription
                                            where s.RegistrationCode.Equals(code)
                                            select s.Id
                                            ).FirstOrDefaultAsync();

                return subscriptionId;
            }
            catch
            {
                return Int32.MinValue;
            }
        }

        public static async Task Register(this IDbSet<UserSubscription> userSubscription, int subscriptionId, string userId)
        {
            try
            {
                if (userSubscription == null || subscriptionId.Equals(Int32.MinValue) || userId.Equals(string.Empty))
                    return;

                // check if the user already has the subscription
                var exist =
                    await Task.Run<int>(() => userSubscription.CountAsync(s => s.SubscriptionId.Equals(subscriptionId) && s.UserId.Equals(userId))) > 0;

                if (!exist)
                    await Task<UserSubscription>.Run(() => userSubscription.Add(
                            new UserSubscription
                            {
                                UserId = userId,
                                SubscriptionId = subscriptionId,
                                StartDate = DateTime.Now,
                                EndDate = DateTime.MaxValue
                            }));
            }
            catch { }
        }

        public static async Task<bool> RegisterUserSubscriptionCode(string code, string userId) {
            try {
                var db = ApplicationDbContext.Create();

                //Make sure that the code is a valid code
                var id = await db.Subscriptions.GetSubscriptionByRegistrationCode(code);

                if (id <= 0) return false;

                //Register the code with the user if it is valid
                await db.UserSubscriptions.Register(id, userId);

                if (db.ChangeTracker.HasChanges())
                    db.SaveChanges();

                return true;
            } catch { return false; }
        }
    }
}