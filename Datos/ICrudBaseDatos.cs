﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public interface ICrudBaseDatos<T>
    {
        List<T> Listar();

        int Registrar(T obj, out String Mensaje);

        bool Editar(T obj, out String Mensaje);

        bool Eliminar(T obj, out String Mensaje);
    }
}