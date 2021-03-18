using System;
using System.Threading.Tasks;
using FluentValidation;

namespace CrowdfindingApp.Common.Extensions
{
    public static class IRuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> WithCustomMessageParameters<T, TProperty>(this IRuleBuilderOptions<T, TProperty> options, Func<T, Task<string[]>> customParams)
        {
            return options.OnAnyFailure((entity, failures) =>
            {
                foreach(var failure in failures)
                {
                    failure.CustomState = customParams.Invoke(entity);
                }
            });
        }
    }
}
