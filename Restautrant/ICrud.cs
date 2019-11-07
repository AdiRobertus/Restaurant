using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restautrant
{
    interface ICrud<T>
    {
        T Data { get; set; }
        bool IsInserting { get; set; }
        void LoadData();
        void SaveData();
        bool IsValid();
        void Clear();
    }
}
