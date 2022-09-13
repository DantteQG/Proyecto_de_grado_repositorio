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
    class Op_confirmadoMap : IEntityTypeConfiguration<Op_confirmado>
    {
        public void Configure(EntityTypeBuilder<Op_confirmado> builder)
        {
            builder.ToTable("op_confirmado")
                .HasKey(oc => oc.idconfirmado);

        }

    }
}
