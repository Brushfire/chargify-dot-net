using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChargifyNET.Json
{
    /// <summary>
    /// Represents a contract resolver that uses Ruby-style lowercase with underscores.
    /// </summary>
    /// <see cref="https://gist.github.com/crallen/9238178"/>
    /// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver" />
    public class SnakeCaseContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Resolved name of the property.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return GetSnakeCase(propertyName);
        }

        /// <summary>
        /// Gets the snake case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string GetSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var buffer = "";

            for (var i = 0; i < input.Length; i++)
            {
                var isLast = (i == input.Length - 1);
                var isSecondFromLast = (i == input.Length - 2);

                var curr = input[i];
                var next = !isLast ? input[i + 1] : '\0';
                var afterNext = !isSecondFromLast && !isLast ? input[i + 2] : '\0';

                buffer += char.ToLower(curr);

                if (!char.IsDigit(curr) && char.IsUpper(next))
                {
                    if (char.IsUpper(curr))
                    {
                        if (!isLast && !isSecondFromLast && !char.IsUpper(afterNext))
                            buffer += "_";
                    }
                    else
                        buffer += "_";
                }

                if (!char.IsDigit(curr) && char.IsDigit(next))
                    buffer += "_";
                if (char.IsDigit(curr) && !char.IsDigit(next) && !isLast)
                    buffer += "_";
            }

            return buffer;
        }
    }
}
