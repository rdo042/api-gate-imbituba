using GateAPI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GateAPI.Domain.Entities.Configuracao
{
    public class Tasks : BaseEntity
    {
        public string Nome { get; private set; } = string.Empty;
        public string Url { get; private set; } = string.Empty;
        public StatusEnum Status { get; private set; }

        private Tasks() { }

        public static Tasks Load(Guid id, string nome, string url, StatusEnum status)
        {
            var entidade = new Tasks
            {
                Nome = nome,
                Url = url,
                Status = status
            };

            entidade.SetId(id);
            return entidade;
        }
    }
}
