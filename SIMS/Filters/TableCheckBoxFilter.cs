using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Filters
{
    abstract class TableCheckBoxFilter<T> : TableFilter<T>
    {
        public bool CheckBoxChecked
        {
            private get;
            set;
        }
        public abstract bool CheckBoxFilter(T entity, bool checkboxChecked);

        protected override bool ShouldAddEntity(T entity)
        {
            return base.ShouldAddEntity(entity) && CheckBoxFilter(entity, CheckBoxChecked);
        }
    }
}
