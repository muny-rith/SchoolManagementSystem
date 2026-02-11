using System.Data;

namespace SchoolManagementSystem
{
    public static class DataReaderExtensions
    {
        public static bool HasColumn(this IDataRecord reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        public static T GetSafe<T>(this IDataRecord reader, string columnName)
        {
            if (!reader.HasColumn(columnName) || reader[columnName] == DBNull.Value)
                return default(T);

            return (T)reader[columnName];
        }

        public static string GetSafeString(this IDataRecord reader, string columnName)
        {
            return reader.HasColumn(columnName) && reader[columnName] != DBNull.Value
                ? reader[columnName].ToString()
                : string.Empty;
        }
        
    }

    
}
