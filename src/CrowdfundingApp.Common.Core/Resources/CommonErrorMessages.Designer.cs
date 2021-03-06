﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrowdfundingApp.Common.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CommonErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CommonErrorMessages() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CrowdfundingApp.Common.Core.Resources.CommonErrorMessages", typeof(CommonErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Идентификатор не должен быть пустым.
        /// </summary>
        internal static string EmptyId {
            get {
                return ResourceManager.GetString("EmptyId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Категории с Id: &quot;{0}&quot; не найдено.
        /// </summary>
        internal static string InvalidCategoryId {
            get {
                return ResourceManager.GetString("InvalidCategoryId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Невалидный формат Id: &quot;{0}&quot;.
        /// </summary>
        internal static string InvalidIdFormat {
            get {
                return ResourceManager.GetString("InvalidIdFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вы неавторизованы.
        /// </summary>
        internal static string NotAuthorizedMessageKey {
            get {
                return ResourceManager.GetString("NotAuthorizedMessageKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект не найден.
        /// </summary>
        internal static string ObjectNotFoundMessageKey {
            get {
                return ResourceManager.GetString("ObjectNotFoundMessageKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Доступ запрещен.
        /// </summary>
        internal static string SecurityErrorMessageKey {
            get {
                return ResourceManager.GetString("SecurityErrorMessageKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Системная ошибка.
        /// </summary>
        internal static string SystemErrorMessageKey {
            get {
                return ResourceManager.GetString("SystemErrorMessageKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Невалидный формат Id.
        /// </summary>
        internal static string WrongIdFormat {
            get {
                return ResourceManager.GetString("WrongIdFormat", resourceCulture);
            }
        }
    }
}
