using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя является обязательным")]
        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Длина строки от 3 до 200 символов")] //Для проверки диапазона длины сообщения
        [MinLength(3, ErrorMessage = "Должно быть более 3 символов")]
        [RegularExpression(@"(?:[А-ЯЁ][а-яё]+)|(?:[A-Z][a-z]+)", ErrorMessage = "Ошибка формата имени - либо кириллица, латиница")] //Проверка по регулярныому выражению
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия является обязательным")]
        [MinLength(3, ErrorMessage = "Должно быть более 3 символов")]
        public string SecondName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возрост")]
        [Required(ErrorMessage = "Возрост обязателен")]
        [Range(18, 75, ErrorMessage = "Возрост должен быть в интервале от 18 до 75")]
        public int Age { get; set; }

        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Телефон обязателено заполнить")]
        [MinLength(11, ErrorMessage = "Цифор должно быть не менее 11")]
        public string Telephone { get; set; }

        [Display(Name = "День рождения")]
        [Required(ErrorMessage = "День рождение является обязательным")]
        public DateTime BirthDay { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Логин обязателено заполнить")]
        [MinLength(11, ErrorMessage = "Минимальная длина 5")]
        public string Login { get; set; }

        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пароль является обязательным")]
        public string Password { get; set; }
    }
}
