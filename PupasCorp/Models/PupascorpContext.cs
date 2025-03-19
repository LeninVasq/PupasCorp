using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PupasCorp.Models;

public partial class PupascorpContext : DbContext
{
    public PupascorpContext()
    {
    }

    public PupascorpContext(DbContextOptions<PupascorpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasIngrediente> CategoriasIngredientes { get; set; }

    public virtual DbSet<Direccione> Direcciones { get; set; }

    public virtual DbSet<Entrega> Entregas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<Ingrediente> Ingredientes { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuIngrediente> MenuIngredientes { get; set; }

    public virtual DbSet<MetodoEntrega> MetodoEntregas { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<PedidosItem> PedidosItems { get; set; }

    public virtual DbSet<Platillo> Platillos { get; set; }

    public virtual DbSet<PlatilloMenu> PlatilloMenus { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; }

    public virtual DbSet<UnidadMedidum> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=PupasCorn_Context");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasIngrediente>(entity =>
        {
            entity.HasKey(e => e.IdCategoriasIngredientes).HasName("PK__Categori__32EC87ED5D269CC5");

            entity.ToTable("Categorias_Ingredientes");

            entity.Property(e => e.IdCategoriasIngredientes).HasColumnName("Id_categorias_ingredientes");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Foto).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccione>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__62C319097BA405DE");

            entity.Property(e => e.IdDireccion).HasColumnName("Id_direccion");
            entity.Property(e => e.Direccion).HasColumnType("text");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direcciones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__Id_us__3E52440B");
        });

        modelBuilder.Entity<Entrega>(entity =>
        {
            entity.HasKey(e => e.IdEntrega).HasName("PK__Entrega__866FA846C0C46A8E");

            entity.ToTable("Entrega");

            entity.Property(e => e.IdEntrega).HasColumnName("Id_entrega");
            entity.Property(e => e.CostoEnvio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Costo_envio");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_entrega");
            entity.Property(e => e.FechaEnvio)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_envio");
            entity.Property(e => e.IdDireccion).HasColumnName("Id_direccion");
            entity.Property(e => e.IdEstadoPedido).HasColumnName("Id_estado_pedido");
            entity.Property(e => e.IdMetodoEntrega).HasColumnName("Id_metodo_entrega");
            entity.Property(e => e.IdPedido).HasColumnName("Id_pedido");
            entity.Property(e => e.IdUsuarioDelivery).HasColumnName("Id_usuario_delivery");

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__Id_dire__72C60C4A");

            entity.HasOne(d => d.IdEstadoPedidoNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdEstadoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__Id_esta__71D1E811");

            entity.HasOne(d => d.IdMetodoEntregaNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdMetodoEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__Id_meto__6FE99F9F");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__Id_pedi__70DDC3D8");

            entity.HasOne(d => d.IdUsuarioDeliveryNavigation).WithMany(p => p.Entregas)
                .HasForeignKey(d => d.IdUsuarioDelivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Entrega__Id_usua__73BA3083");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstadoPedido).HasName("PK__Estado_p__6A5EC235D6A5DDA5");

            entity.ToTable("Estado_pedido");

            entity.Property(e => e.IdEstadoPedido).HasColumnName("Id_estado_pedido");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.EstadoPedido1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Estado_pedido");
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Factura__A6DB936212B09434");

            entity.ToTable("Factura");

            entity.Property(e => e.IdFactura).HasColumnName("Id_factura");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaEmision)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_emision");
            entity.Property(e => e.IdEntrega).HasColumnName("Id_entrega");
            entity.Property(e => e.IdMetodoPago).HasColumnName("Id_metodo_pago");
            entity.Property(e => e.Iva).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Observaciones).HasColumnType("text");
            entity.Property(e => e.SubTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Sub_total");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdEntregaNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdEntrega)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__Id_entr__7A672E12");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Factura__Id_meto__7B5B524B");
        });

        modelBuilder.Entity<Ingrediente>(entity =>
        {
            entity.HasKey(e => e.IdIngrediente).HasName("PK__Ingredie__72803E72526FA4FD");

            entity.Property(e => e.IdIngrediente).HasColumnName("Id_ingrediente");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Foto).HasColumnType("text");
            entity.Property(e => e.IdCategoriasIngredientes).HasColumnName("Id_categorias_ingredientes");
            entity.Property(e => e.IdUnidadMedida).HasColumnName("Id_unidad_medida");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCategoriasIngredientesNavigation).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.IdCategoriasIngredientes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__Id_ca__48CFD27E");

            entity.HasOne(d => d.IdUnidadMedidaNavigation).WithMany(p => p.Ingredientes)
                .HasForeignKey(d => d.IdUnidadMedida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Ingredien__Id_un__47DBAE45");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__E1E456B2B9B96B08");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("Id_menu");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Foto).HasColumnType("text");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MenuIngrediente>(entity =>
        {
            entity.HasKey(e => e.IdMenuIngredi).HasName("PK__Menu_ing__6E64080C96133BFC");

            entity.ToTable("Menu_ingredientes");

            entity.Property(e => e.IdMenuIngredi).HasColumnName("Id_menu_ingredi");
            entity.Property(e => e.IdIngrediente).HasColumnName("Id_ingrediente");
            entity.Property(e => e.IdMenu).HasColumnName("Id_menu");

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.MenuIngredientes)
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu_ingr__Id_in__5629CD9C");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.MenuIngredientes)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Menu_ingr__Id_me__571DF1D5");
        });

        modelBuilder.Entity<MetodoEntrega>(entity =>
        {
            entity.HasKey(e => e.IdMetodoEntrega).HasName("PK__Metodo_e__224ACA641BEA26B7");

            entity.ToTable("Metodo_entrega");

            entity.Property(e => e.IdMetodoEntrega).HasColumnName("Id_metodo_entrega");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.MetodoEntrega1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Metodo_entrega");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__Metodo_p__574DD6A84D503ECD");

            entity.ToTable("Metodo_pago");

            entity.Property(e => e.IdMetodoPago).HasColumnName("Id_metodo_pago");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.IdMovimientos).HasName("PK__Movimien__B0F5CCB78B82A6D4");

            entity.Property(e => e.IdMovimientos).HasColumnName("Id_movimientos");
            entity.Property(e => e.CostoUnitario)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Costo_unitario");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_vencimiento");
            entity.Property(e => e.IdIngrediente).HasColumnName("Id_ingrediente");
            entity.Property(e => e.IdTipoMovimientos).HasColumnName("Id_tipo_movimientos");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdIngredienteNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdIngrediente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__Id_in__4E88ABD4");

            entity.HasOne(d => d.IdTipoMovimientosNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdTipoMovimientos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__Id_ti__4F7CD00D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Movimient__Id_us__5070F446");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido).HasName("PK__Pedido__1E657E9E2DE4A24A");

            entity.ToTable("Pedido");

            entity.Property(e => e.IdPedido).HasColumnName("Id_pedido");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("Fecha_entrega");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Pedido__Id_usuar__628FA481");
        });

        modelBuilder.Entity<PedidosItem>(entity =>
        {
            entity.HasKey(e => e.IdPedidosItem).HasName("PK__Pedidos___FA4C9C007B6A4E7E");

            entity.ToTable("Pedidos_item");

            entity.Property(e => e.IdPedidosItem).HasColumnName("Id_pedidos_item");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.IdPedido).HasColumnName("Id_pedido");
            entity.Property(e => e.IdPlatillo).HasColumnName("Id_platillo");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.PedidosItems)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Pedidos_i__Id_pe__66603565");

            entity.HasOne(d => d.IdPlatilloNavigation).WithMany(p => p.PedidosItems)
                .HasForeignKey(d => d.IdPlatillo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Pedidos_i__Id_pl__6754599E");
        });

        modelBuilder.Entity<Platillo>(entity =>
        {
            entity.HasKey(e => e.IdPlatillo).HasName("PK__Platillo__F5F96BC3A305C686");

            entity.Property(e => e.IdPlatillo).HasColumnName("Id_platillo");
            entity.Property(e => e.Foto).HasColumnType("text");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Mostrar).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Platillos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Platillos__Id_us__5AEE82B9");
        });

        modelBuilder.Entity<PlatilloMenu>(entity =>
        {
            entity.HasKey(e => e.IdPlatilloMenu).HasName("PK__Platillo__DB2847EDF2F69281");

            entity.ToTable("Platillo_menu");

            entity.Property(e => e.IdPlatilloMenu).HasColumnName("Id_platillo_menu");
            entity.Property(e => e.IdMenu).HasColumnName("Id_menu");
            entity.Property(e => e.IdPlatillo).HasColumnName("Id_platillo");

            entity.HasOne(d => d.IdMenuNavigation).WithMany(p => p.PlatilloMenus)
                .HasForeignKey(d => d.IdMenu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Platillo___Id_me__5EBF139D");

            entity.HasOne(d => d.IdPlatilloNavigation).WithMany(p => p.PlatilloMenus)
                .HasForeignKey(d => d.IdPlatillo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Platillo___Id_pl__5DCAEF64");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTipoUsuario).HasName("PK__Tipo_usu__C05DF7FE26D22B50");

            entity.ToTable("Tipo_usuario");

            entity.Property(e => e.IdTipoUsuario).HasColumnName("Id_tipo_usuario");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TiposMovimiento>(entity =>
        {
            entity.HasKey(e => e.IdTipoMovimientos).HasName("PK__Tipos_mo__845FEA2E8ABE3528");

            entity.ToTable("Tipos_movimientos");

            entity.Property(e => e.IdTipoMovimientos).HasColumnName("Id_tipo_movimientos");
            entity.Property(e => e.TipoMovimientos)
                .HasColumnType("text")
                .HasColumnName("Tipo_movimientos");
        });

        modelBuilder.Entity<UnidadMedidum>(entity =>
        {
            entity.HasKey(e => e.IdUnidadMedida).HasName("PK__Unidad_m__525F61E2FF9A2D38");

            entity.ToTable("Unidad_medida");

            entity.Property(e => e.IdUnidadMedida).HasColumnName("Id_unidad_medida");
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__EF59F76221CC9F06");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Contrasenia).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Foto).HasColumnType("text");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("Id_tipo_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Token).HasColumnType("text");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .HasConstraintName("FK__Usuario__Id_tipo__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
