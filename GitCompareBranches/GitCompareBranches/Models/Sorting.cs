using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitCompareBranches.Models
{
    public sealed class SpecificationForSortingPropertiesOrFields<T> : IComparer<T>
    {
        private string[] sortColumns;
        private bool[] arrayAscending;

        public SpecificationForSortingPropertiesOrFields()
        {
            //Specify sort columns at run time by calling the sub SetSortColumns()
        }
        public void SetSortColumns(string[] colSortColumns, bool boolAscending)
        {
            this.sortColumns = colSortColumns;
            arrayAscending = new bool[colSortColumns.Length];
            for (int i = 0; i < arrayAscending.Length; i++) arrayAscending[i] = boolAscending;
            CreateDictionaries();

        }
        public SpecificationForSortingPropertiesOrFields(string sortColumn, bool boolAscending)
            : this(new string[] { sortColumn }, new bool[] { boolAscending })
        {
        }
        public SpecificationForSortingPropertiesOrFields(string[] strSortColumns, bool[] boolAscending)
        {
            this.sortColumns = strSortColumns;
            this.arrayAscending = boolAscending;
            CreateDictionaries();
        }
        public SpecificationForSortingPropertiesOrFields(string[] colSortColumns, bool boolAscending)
        {
            this.sortColumns = colSortColumns;
            arrayAscending = new bool[colSortColumns.Length];
            for (int i = 0; i < arrayAscending.Length; i++) arrayAscending[i] = boolAscending;
            CreateDictionaries();

        }
        private Dictionary<string, System.Reflection.PropertyInfo> dicProperties = new Dictionary<string, System.Reflection.PropertyInfo>();
        private Dictionary<string, System.Reflection.FieldInfo> dicFields = new Dictionary<string, System.Reflection.FieldInfo>();
        private char space = ' ';
        private void CreateDictionaries()
        {
            //Build a dictionary of properties
            foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
            {
                string columnName = property.Name.ToString().Split(space)[0];
                if (Array.IndexOf(sortColumns, columnName) == -1) continue;
                dicProperties.Add(columnName, property);
            }
            //Build a dictionary of fields
            foreach (System.Reflection.FieldInfo Field in typeof(T).GetFields())
            {
                string columnName = Field.Name.ToString().Split(space)[0];
                if (Array.IndexOf(sortColumns, columnName) == -1) continue;
                dicFields.Add(columnName, Field);
            }
        }
        public int Compare(T x, T y)
        {
            int result = 0;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                string sortCol = sortColumns[i];
                bool Asc = arrayAscending[i];
                IComparable obj1 = null;
                IComparable obj2 = null;
                if (dicProperties.ContainsKey(sortCol))
                {
                    System.Reflection.PropertyInfo propInfo = dicProperties[sortCol];
                    obj1 = (IComparable)propInfo.GetValue(x, null);
                    obj2 = (IComparable)propInfo.GetValue(y, null);
                }
                else
                {
                    System.Reflection.FieldInfo oFieldInfo = dicFields[sortCol];
                    obj1 = (IComparable)oFieldInfo.GetValue(x);
                    obj2 = (IComparable)oFieldInfo.GetValue(y);
                }
                if (Asc) result = obj1.CompareTo(obj2); else result = obj2.CompareTo(obj1);
                if (result != 0) return result;
            }
            return result;
        }
    }

}
