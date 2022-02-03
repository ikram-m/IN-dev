using CsvHelper.Configuration;
using E5irProjet.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace E5irProjet.Mapper
{
    public sealed class RadioMapp : ClassMap<Radio>
    {
        public RadioMapp()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Name_file).Name("File Name");
            Map(m => m.Header).Name("Field Name");
            Map(m => m.Status).Name("Status");

        }
    }
}
