using SIMS.Model;
using System;

namespace SIMS.Filters
{
    class NotificationsFilter : TableFilter<Notification, NotificationsFilter>
    {
        public override bool CheckBoxFilter(Notification entity, bool checkboxChecked)
        {
            return true;
        }

        public override bool KeywordFilter(Notification entity, string keyword)
        {
            return entity.Author.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.Content.Contains(keyword, StringComparison.InvariantCultureIgnoreCase) ||
                   entity.DateAndTimeString.Contains(keyword, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
