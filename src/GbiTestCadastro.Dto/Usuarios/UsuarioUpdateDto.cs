﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbiTestCadastro.Dto.Usuarios
{
    public class UsuarioUpdateDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Senha { get; set; }
    }
}
