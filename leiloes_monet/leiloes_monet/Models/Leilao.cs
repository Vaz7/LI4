﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace leiloes_monet.Models
{
    public class Leilao
    {
        public int Id { get; set; }
       
        public string nome { get; set; }
        public DateTime data_inicio { get; set; }
        public DateTime data_fim { get; set; }
        public bool estado { get; set; }
        public double valor_base { get; set; }
        public Utilizador utilizador { get; set; }
        public Quadro quadro { get; set; }
    }
}
