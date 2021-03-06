﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrowdfundingApp.Core.Services.Orders.Resources {
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
    internal class OrderErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal OrderErrorMessages() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CrowdfundingApp.Core.Services.Orders.Resources.OrderErrorMessages", typeof(OrderErrorMessages).Assembly);
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
        ///   Ищет локализованную строку, похожую на Проект не доступен для поддержки.
        /// </summary>
        internal static string OrderErrorMessageKeys_DisallowToSupportProject {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_DisallowToSupportProject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Введите CVC2/CVV2.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyCvv {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyCvv", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Заполните поле &quot;Полный адрес&quot;.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyFullAddress {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyFullAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Заполните поле &quot;Отчество получателя&quot;.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyMiddleName {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyMiddleName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Заполните поле &quot;Имя получателя&quot;.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyName {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Введите срок действия платежной карты.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyPayCardExpirationDate {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyPayCardExpirationDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Введите номер платежной карты.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyPayCardNumber {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyPayCardNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Введите имя владельца карты.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyPayCardOwnerName {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyPayCardOwnerName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Заполните поле &quot;Почтовый индекс&quot;.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptyPostCode {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptyPostCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Заполните поле Фамилия получателя&quot;.
        /// </summary>
        internal static string OrderErrorMessageKeys_EmptySurname {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_EmptySurname", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Вы превысили ограничение. Количество должно быть меньше чем {0}.
        /// </summary>
        internal static string OrderErrorMessageKeys_GreaterThanLimit {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_GreaterThanLimit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Количество вознаграждении должно быть больше чем 0.
        /// </summary>
        internal static string OrderErrorMessageKeys_RewardCountLessThanOne {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_RewardCountLessThanOne", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Направильный формат CVC2/CVV2. Ожидалось 3 цифры.
        /// </summary>
        internal static string OrderErrorMessageKeys_WrongCvvValue {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_WrongCvvValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Неверный срок действия платежной карты.
        /// </summary>
        internal static string OrderErrorMessageKeys_WrongPayCardExpirationDate {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_WrongPayCardExpirationDate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Неправильны формат номера платежной карты. Ожидалось 16 цифр.
        /// </summary>
        internal static string OrderErrorMessageKeys_WrongPayCardNumber {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_WrongPayCardNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Неправльный формат имени владельца карты. Ожидалось до 20 латинских букв.
        /// </summary>
        internal static string OrderErrorMessageKeys_WrongPayCardOwnerName {
            get {
                return ResourceManager.GetString("OrderErrorMessageKeys_WrongPayCardOwnerName", resourceCulture);
            }
        }
    }
}
