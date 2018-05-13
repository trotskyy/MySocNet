using System;
using System.Collections.Generic;
using System.Text;

namespace MySocNet.Bll.Services.Abstract
{
    public interface ISecurityProvider
    {
        /// <summary>
        /// Высчитать хэш сумму строки
        /// </summary>
        string GenerateHash(string original);
        /// <summary>
        /// Высчитать хэш сумму строки с солью
        /// </summary>
        string GenerateHash(string original, string salt);
        /// <summary>
        /// Сгенерировать соль для добавления в конец пароля
        /// </summary>
        /// <param name="size">Длинна</param>
        /// <returns>Соль - криптиографически правильно рандомная строка</returns>
        string GenerateSalt(int size);
    }
}
