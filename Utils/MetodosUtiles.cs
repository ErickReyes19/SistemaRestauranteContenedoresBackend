namespace RestauranteBackend.Utils
{
    public static class MetodosUtiles
    {
        public static void ActualizarPropiedades<T>(this T target, T source)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (!property.CanWrite || !property.CanRead)
                    continue;

                var newValue = property.GetValue(source);
                if (newValue != null)
                {
                    property.SetValue(target, newValue);
                }
            }
        }
    }
}
