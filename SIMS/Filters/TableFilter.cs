using SIMS.Repositories.SecretaryRepo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SIMS.Filters
{
    abstract class TableFilter<T, TableFilterType>
        where TableFilterType : TableFilter<T, TableFilterType>, new()
    {

        private static TableFilterType _instance = new TableFilterType();
        public static TableFilterType Instance
        {
            get
            {
                return _instance;
            }
        }

        public abstract bool KeywordFilter(T entity, string keyword);
        public abstract bool CheckBoxFilter(T entity, bool checkboxChecked);
        public ObservableCollection<T> ApplyFilters(ObservableCollection<T> unfiltered, string input, bool checkboxChecked)
        {
            ObservableCollection<T> filtered = new ObservableCollection<T>();
            var keywords = input.Split(" ");
            foreach (T entity in unfiltered)
            {
                if (!CheckBoxFilter(entity, checkboxChecked))
                {
                    goto NEXT;
                }
                foreach (string keyword in keywords)
                {
                    if (!KeywordFilter(entity, keyword))
                    {
                        goto NEXT;
                    }
                }
                filtered.Add(entity);
            NEXT:;
            }
            return filtered;
        }

    }
}
