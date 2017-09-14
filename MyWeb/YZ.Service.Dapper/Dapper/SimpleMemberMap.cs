using System;
using System.Reflection;

namespace Dapper
{
    /// <summary>
    /// Represents simple member map for one of target parameter or property or field to source DataReader column
    /// </summary>
    sealed class SimpleMemberMap : SqlMapper.IMemberMap
    {
        /// <summary>
        /// Creates instance for simple property mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="property">Target property</param>
        public SimpleMemberMap(string columnName, PropertyInfo property)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (property == null)
                throw new ArgumentNullException("property");

            _columnname = columnName;
            _property = property;
        }

        /// <summary>
        /// Creates instance for simple field mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="field">Target property</param>
        public SimpleMemberMap(string columnName, FieldInfo field)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (field == null)
                throw new ArgumentNullException("field");

            _columnname = columnName;
            _field = field;
        }

        /// <summary>
        /// Creates instance for simple constructor parameter mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="parameter">Target constructor parameter</param>
        public SimpleMemberMap(string columnName, ParameterInfo parameter)
        {
            if (columnName == null)
                throw new ArgumentNullException("columnName");

            if (parameter == null)
                throw new ArgumentNullException("parameter");

            _columnname = columnName;
            _parameter = parameter;
        }



        /// <summary>
        /// Target member type
        /// </summary>
        //public Type MemberType => Field?.FieldType ?? Property?.PropertyType ?? Parameter?.ParameterType;
        public Type MemberType
        {
            get { return Field != null ? Field.FieldType : Property != null ? Property.PropertyType : Parameter != null ? Parameter.ParameterType : null; }
            set { }
        }


        public readonly string _columnname;
        public readonly PropertyInfo _property;
        public readonly FieldInfo _field;
        public readonly ParameterInfo _parameter;

        /// <summary>
        /// DataReader column name
        /// </summary>
        public string ColumnName { get { return _columnname; } }
        /// <summary>
        /// Target property
        /// </summary>
        public PropertyInfo Property { get { return _property; } }

        /// <summary>
        /// Target field
        /// </summary>
        public FieldInfo Field { get { return _field; } }

        /// <summary>
        /// Target constructor parameter
        /// </summary>
        public ParameterInfo Parameter { get { return _parameter; } }
    }
}
