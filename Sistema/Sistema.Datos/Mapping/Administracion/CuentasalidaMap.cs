using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Administracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Administracion
{
    class CuentasalidaMap : IEntityTypeConfiguration<Cuentasalida>
    {
        public void Configure(EntityTypeBuilder<Cuentasalida> builder)
        {
            builder.ToTable("cuentasalida")
                .HasKey(cs => cs.idcuentasalida);

        }
        
    }
}
