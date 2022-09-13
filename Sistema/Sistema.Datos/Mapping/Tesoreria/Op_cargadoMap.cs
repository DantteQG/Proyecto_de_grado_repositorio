using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Tesoreria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Tesoreria
{
    class Op_cargadoMap : IEntityTypeConfiguration<Op_cargado>
    {
        public void Configure(EntityTypeBuilder<Op_cargado> builder)
        {
            builder.ToTable("op_cargado")
                .HasKey(oc => oc.idcargado);

        }

    }
}
