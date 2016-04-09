﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApplication.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Проверить уникальность.
        /// </summary>
        public static string CheckUserName {
            get {
                return ResourceManager.GetString("CheckUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Email.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Email или Имя пользователя.
        /// </summary>
        public static string EmailOrNickName {
            get {
                return ResourceManager.GetString("EmailOrNickName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Для авторизации необходимо подтвердить Email {0}.
        /// </summary>
        public static string ErrorEmailNotConfirmed {
            get {
                return ResourceManager.GetString("ErrorEmailNotConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Минимальная длина {1}.
        /// </summary>
        public static string ErrorMinLength {
            get {
                return ResourceManager.GetString("ErrorMinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Пароль и его подтверждение не совпадают.
        /// </summary>
        public static string ErrorPasswordsCompare {
            get {
                return ResourceManager.GetString("ErrorPasswordsCompare", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на {0} является обязательным полем.
        /// </summary>
        public static string ErrorRequired {
            get {
                return ResourceManager.GetString("ErrorRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Выберите файл скина.
        /// </summary>
        public static string ErrorSkinEmpty {
            get {
                return ResourceManager.GetString("ErrorSkinEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Скин должен быть формата png.
        /// </summary>
        public static string ErrorSkinFormat {
            get {
                return ResourceManager.GetString("ErrorSkinFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Минимальная длина {2}.
        /// </summary>
        public static string ErrorStringMinLength {
            get {
                return ResourceManager.GetString("ErrorStringMinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя пользователя не может быть пустым.
        /// </summary>
        public static string ErrorUserNameEmpty {
            get {
                return ResourceManager.GetString("ErrorUserNameEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Пользователь с таким именем уже существует.
        /// </summary>
        public static string ErrorUserNameExists {
            get {
                return ResourceManager.GetString("ErrorUserNameExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Пол.
        /// </summary>
        public static string Gender {
            get {
                return ResourceManager.GetString("Gender", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Женский.
        /// </summary>
        public static string GenderFemale {
            get {
                return ResourceManager.GetString("GenderFemale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Мужской.
        /// </summary>
        public static string GenderMale {
            get {
                return ResourceManager.GetString("GenderMale", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Имя пользователя.
        /// </summary>
        public static string NickName {
            get {
                return ResourceManager.GetString("NickName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Пароль.
        /// </summary>
        public static string Password {
            get {
                return ResourceManager.GetString("Password", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Подтверждение пароля.
        /// </summary>
        public static string PasswordConfirm {
            get {
                return ResourceManager.GetString("PasswordConfirm", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Это имя не занято.
        /// </summary>
        public static string UserNameNoExists {
            get {
                return ResourceManager.GetString("UserNameNoExists", resourceCulture);
            }
        }
    }
}
