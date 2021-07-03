using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using CrowdfindingApp.Common.Extensions;

namespace CrowdfindingApp.Common.Core.Localization
{
    public class ResourceProvider : IResourceProvider
    {
        private readonly List<(string, ResourceManager)> _resourceManagers = new List<(string, ResourceManager)>();

        public ResourceProvider() : this(new List<(string, Assembly)>()) { }

        public ResourceProvider(List<(string, Assembly)> stringResources)
        {
            foreach(var stringResourceProvider in stringResources)
            {
                _resourceManagers.Add((stringResourceProvider.Item1, new ResourceManager(stringResourceProvider.Item1, stringResourceProvider.Item2 ?? Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly())));
            }
        }

        public string GetString(string key, params object[] args)
        {
            if(key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if(args?.Any() ?? false)
            {
                return string.Format(GetResource(key), args);
            }
            else
            {
                return GetResource(key);
            }
        }

        private string GetResource(string resourceKey)
        {
            foreach(var rm in _resourceManagers.Select(x => x.Item2))
            {
                var value = rm.GetString(resourceKey, CultureInfo.CurrentUICulture);
                if(value.IsPresent())
                {
                    return value;
                }
            }

            return resourceKey;
        }
    }
}
