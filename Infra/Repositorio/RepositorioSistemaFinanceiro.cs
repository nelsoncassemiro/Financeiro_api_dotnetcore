﻿using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Infra.Repositorio.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio
{
    public class RepositorioSistemaFinanceiro : RepositoryGenerics<SistemaFinanceiro>, ISistemaFinanceiro
    {
        public Task<IList<SistemaFinanceiro>> ListaSistemasUsuario(string emailUsuario)
        {
            throw new NotImplementedException();
        }
    }
}
