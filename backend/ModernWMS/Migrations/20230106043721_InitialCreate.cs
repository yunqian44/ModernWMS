using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModernWMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "asn",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    asnno = table.Column<string>(name: "asn_no", type: "TEXT", nullable: false),
                    asnstatus = table.Column<byte>(name: "asn_status", type: "INTEGER", nullable: false),
                    spuid = table.Column<int>(name: "spu_id", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    asnqty = table.Column<int>(name: "asn_qty", type: "INTEGER", nullable: false),
                    actualqty = table.Column<int>(name: "actual_qty", type: "INTEGER", nullable: false),
                    sortedqty = table.Column<int>(name: "sorted_qty", type: "INTEGER", nullable: false),
                    shortageqty = table.Column<int>(name: "shortage_qty", type: "INTEGER", nullable: false),
                    moreqty = table.Column<int>(name: "more_qty", type: "INTEGER", nullable: false),
                    damageqty = table.Column<int>(name: "damage_qty", type: "INTEGER", nullable: false),
                    weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    supplierid = table.Column<int>(name: "supplier_id", type: "INTEGER", nullable: false),
                    suppliername = table.Column<string>(name: "supplier_name", type: "TEXT", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodsownername = table.Column<string>(name: "goods_owner_name", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asn", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "asnsort",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    asnid = table.Column<int>(name: "asn_id", type: "INTEGER", nullable: false),
                    sortedqty = table.Column<int>(name: "sorted_qty", type: "INTEGER", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asnsort", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    categoryname = table.Column<string>(name: "category_name", type: "TEXT", nullable: false),
                    parentid = table.Column<int>(name: "parent_id", type: "INTEGER", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    companyname = table.Column<string>(name: "company_name", type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    manager = table.Column<string>(type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    customername = table.Column<string>(name: "customer_name", type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    manager = table.Column<string>(type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dispatchlist",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dispatchno = table.Column<string>(name: "dispatch_no", type: "TEXT", nullable: false),
                    dispatchstatus = table.Column<byte>(name: "dispatch_status", type: "INTEGER", nullable: false),
                    customerid = table.Column<int>(name: "customer_id", type: "INTEGER", nullable: false),
                    customername = table.Column<string>(name: "customer_name", type: "TEXT", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    damageqty = table.Column<int>(name: "damage_qty", type: "INTEGER", nullable: false),
                    lockqty = table.Column<int>(name: "lock_qty", type: "INTEGER", nullable: false),
                    pickedqty = table.Column<int>(name: "picked_qty", type: "INTEGER", nullable: false),
                    intrasitqty = table.Column<int>(name: "intrasit_qty", type: "INTEGER", nullable: false),
                    packageqty = table.Column<int>(name: "package_qty", type: "INTEGER", nullable: false),
                    weighingqty = table.Column<int>(name: "weighing_qty", type: "INTEGER", nullable: false),
                    actualqty = table.Column<int>(name: "actual_qty", type: "INTEGER", nullable: false),
                    signqty = table.Column<int>(name: "sign_qty", type: "INTEGER", nullable: false),
                    packageno = table.Column<string>(name: "package_no", type: "TEXT", nullable: false),
                    packageperson = table.Column<string>(name: "package_person", type: "TEXT", nullable: false),
                    packagetime = table.Column<DateTime>(name: "package_time", type: "TEXT", nullable: false),
                    weighingno = table.Column<string>(name: "weighing_no", type: "TEXT", nullable: false),
                    weighingperson = table.Column<string>(name: "weighing_person", type: "TEXT", nullable: false),
                    weighingweight = table.Column<decimal>(name: "weighing_weight", type: "TEXT", nullable: false),
                    waybillno = table.Column<string>(name: "waybill_no", type: "TEXT", nullable: false),
                    carrier = table.Column<string>(type: "TEXT", nullable: false),
                    freightfee = table.Column<decimal>(type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispatchlist", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "freightfee",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    carrier = table.Column<string>(type: "TEXT", nullable: false),
                    departurecity = table.Column<string>(name: "departure_city", type: "TEXT", nullable: false),
                    arrivalcity = table.Column<string>(name: "arrival_city", type: "TEXT", nullable: false),
                    priceperweight = table.Column<decimal>(name: "price_per_weight", type: "TEXT", nullable: false),
                    pricepervolume = table.Column<decimal>(name: "price_per_volume", type: "TEXT", nullable: false),
                    minpayment = table.Column<decimal>(name: "min_payment", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freightfee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "goodslocation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    warehouseid = table.Column<int>(name: "warehouse_id", type: "INTEGER", nullable: false),
                    warehousename = table.Column<string>(name: "warehouse_name", type: "TEXT", nullable: false),
                    warehouseareaname = table.Column<string>(name: "warehouse_area_name", type: "TEXT", nullable: false),
                    warehouseareaproperty = table.Column<byte>(name: "warehouse_area_property", type: "INTEGER", nullable: false),
                    locationname = table.Column<string>(name: "location_name", type: "TEXT", nullable: false),
                    locationlength = table.Column<decimal>(name: "location_length", type: "TEXT", nullable: false),
                    locationwidth = table.Column<decimal>(name: "location_width", type: "TEXT", nullable: false),
                    locationheigth = table.Column<decimal>(name: "location_heigth", type: "TEXT", nullable: false),
                    locationvolume = table.Column<decimal>(name: "location_volume", type: "TEXT", nullable: false),
                    locationload = table.Column<decimal>(name: "location_load", type: "TEXT", nullable: false),
                    roadwaynumber = table.Column<string>(name: "roadway_number", type: "TEXT", nullable: false),
                    shelfnumber = table.Column<string>(name: "shelf_number", type: "TEXT", nullable: false),
                    layernumber = table.Column<string>(name: "layer_number", type: "TEXT", nullable: false),
                    tagnumber = table.Column<string>(name: "tag_number", type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false),
                    warehouseareaid = table.Column<int>(name: "warehouse_area_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goodslocation", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "goodsowner",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    goodsownername = table.Column<string>(name: "goods_owner_name", type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    manager = table.Column<string>(type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_goodsowner", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    menuname = table.Column<string>(name: "menu_name", type: "TEXT", nullable: false),
                    module = table.Column<string>(type: "TEXT", nullable: false),
                    vuepath = table.Column<string>(name: "vue_path", type: "TEXT", nullable: false),
                    vuepathdetail = table.Column<string>(name: "vue_path_detail", type: "TEXT", nullable: false),
                    vuedirectory = table.Column<string>(name: "vue_directory", type: "TEXT", nullable: false),
                    sort = table.Column<int>(type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rolemenu",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userroleid = table.Column<int>(name: "userrole_id", type: "INTEGER", nullable: false),
                    menuid = table.Column<int>(name: "menu_id", type: "INTEGER", nullable: false),
                    authority = table.Column<byte>(type: "INTEGER", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolemenu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "spu",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    spucode = table.Column<string>(name: "spu_code", type: "TEXT", nullable: false),
                    spuname = table.Column<string>(name: "spu_name", type: "TEXT", nullable: false),
                    categoryid = table.Column<int>(name: "category_id", type: "INTEGER", nullable: false),
                    spudescription = table.Column<string>(name: "spu_description", type: "TEXT", nullable: false),
                    barcode = table.Column<string>(name: "bar_code", type: "TEXT", nullable: false),
                    supplierid = table.Column<int>(name: "supplier_id", type: "INTEGER", nullable: false),
                    suppliername = table.Column<string>(name: "supplier_name", type: "TEXT", nullable: false),
                    brand = table.Column<string>(type: "TEXT", nullable: false),
                    origin = table.Column<string>(type: "TEXT", nullable: false),
                    lengthunit = table.Column<byte>(name: "length_unit", type: "INTEGER", nullable: false),
                    volumeunit = table.Column<byte>(name: "volume_unit", type: "INTEGER", nullable: false),
                    weightunit = table.Column<byte>(name: "weight_unit", type: "INTEGER", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    isfreeze = table.Column<bool>(name: "is_freeze", type: "INTEGER", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stockadjust",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobcode = table.Column<string>(name: "job_code", type: "TEXT", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false),
                    isupdatestock = table.Column<bool>(name: "is_update_stock", type: "INTEGER", nullable: false),
                    jobtype = table.Column<byte>(name: "job_type", type: "INTEGER", nullable: false),
                    sourcetableid = table.Column<int>(name: "source_table_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockadjust", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stockfreeze",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobcode = table.Column<string>(name: "job_code", type: "TEXT", nullable: false),
                    jobtype = table.Column<bool>(name: "job_type", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    handler = table.Column<string>(type: "TEXT", nullable: false),
                    handletime = table.Column<DateTime>(name: "handle_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockfreeze", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stockmove",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobcode = table.Column<string>(name: "job_code", type: "TEXT", nullable: false),
                    movestatus = table.Column<byte>(name: "move_status", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    origgoodslocationid = table.Column<int>(name: "orig_goods_location_id", type: "INTEGER", nullable: false),
                    destgoogslocationid = table.Column<int>(name: "dest_googs_location_id", type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    handler = table.Column<string>(type: "TEXT", nullable: false),
                    handletime = table.Column<DateTime>(name: "handle_time", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockmove", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stockprocess",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobcode = table.Column<string>(name: "job_code", type: "TEXT", nullable: false),
                    jobtype = table.Column<bool>(name: "job_type", type: "INTEGER", nullable: false),
                    processstatus = table.Column<bool>(name: "process_status", type: "INTEGER", nullable: false),
                    processor = table.Column<string>(type: "TEXT", nullable: false),
                    processtime = table.Column<DateTime>(name: "process_time", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockprocess", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "stocktaking",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    jobcode = table.Column<string>(name: "job_code", type: "TEXT", nullable: false),
                    jobstatus = table.Column<bool>(name: "job_status", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    bookqty = table.Column<int>(name: "book_qty", type: "INTEGER", nullable: false),
                    countedqty = table.Column<int>(name: "counted_qty", type: "INTEGER", nullable: false),
                    differenceqty = table.Column<int>(name: "difference_qty", type: "INTEGER", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false),
                    handler = table.Column<string>(type: "TEXT", nullable: false),
                    handletime = table.Column<DateTime>(name: "handle_time", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocktaking", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    suppliername = table.Column<string>(name: "supplier_name", type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    manager = table.Column<string>(type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_supplier", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usernum = table.Column<string>(name: "user_num", type: "TEXT", nullable: false),
                    username = table.Column<string>(name: "user_name", type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    userrole = table.Column<string>(name: "user_role", type: "TEXT", nullable: false),
                    sex = table.Column<string>(type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    authstring = table.Column<string>(name: "auth_string", type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "userrole",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rolename = table.Column<string>(name: "role_name", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userrole", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    warehousename = table.Column<string>(name: "warehouse_name", type: "TEXT", nullable: false),
                    city = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    manager = table.Column<string>(type: "TEXT", nullable: false),
                    contacttel = table.Column<string>(name: "contact_tel", type: "TEXT", nullable: false),
                    creator = table.Column<string>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouse", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehousearea",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    warehouseid = table.Column<int>(name: "warehouse_id", type: "INTEGER", nullable: false),
                    areaname = table.Column<string>(name: "area_name", type: "TEXT", nullable: false),
                    parentid = table.Column<int>(name: "parent_id", type: "INTEGER", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    isvalid = table.Column<bool>(name: "is_valid", type: "INTEGER", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false),
                    areaproperty = table.Column<byte>(name: "area_property", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehousearea", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dispatchpicklist",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dispatchlistid = table.Column<int>(name: "dispatchlist_id", type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    pickqty = table.Column<int>(name: "pick_qty", type: "INTEGER", nullable: false),
                    pickedqty = table.Column<int>(name: "picked_qty", type: "INTEGER", nullable: false),
                    isupdatestock = table.Column<bool>(name: "is_update_stock", type: "INTEGER", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dispatchpicklist", x => x.id);
                    table.ForeignKey(
                        name: "FK_dispatchpicklist_dispatchlist_dispatchlist_id",
                        column: x => x.dispatchlistid,
                        principalTable: "dispatchlist",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sku",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    spuid = table.Column<int>(name: "spu_id", type: "INTEGER", nullable: false),
                    skucode = table.Column<string>(name: "sku_code", type: "TEXT", nullable: false),
                    skuname = table.Column<string>(name: "sku_name", type: "TEXT", nullable: false),
                    weight = table.Column<decimal>(type: "TEXT", nullable: false),
                    lenght = table.Column<decimal>(type: "TEXT", nullable: false),
                    width = table.Column<decimal>(type: "TEXT", nullable: false),
                    height = table.Column<decimal>(type: "TEXT", nullable: false),
                    volume = table.Column<decimal>(type: "TEXT", nullable: false),
                    unit = table.Column<string>(type: "TEXT", nullable: false),
                    cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    createtime = table.Column<DateTime>(name: "create_time", type: "TEXT", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sku", x => x.id);
                    table.ForeignKey(
                        name: "FK_sku_spu_spu_id",
                        column: x => x.spuid,
                        principalTable: "spu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stockprocessdetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    stockprocessid = table.Column<int>(name: "stock_process_id", type: "INTEGER", nullable: false),
                    skuid = table.Column<int>(name: "sku_id", type: "INTEGER", nullable: false),
                    goodsownerid = table.Column<int>(name: "goods_owner_id", type: "INTEGER", nullable: false),
                    goodslocationid = table.Column<int>(name: "goods_location_id", type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    lastupdatetime = table.Column<DateTime>(name: "last_update_time", type: "TEXT", nullable: false),
                    tenantid = table.Column<long>(name: "tenant_id", type: "INTEGER", nullable: false),
                    issource = table.Column<bool>(name: "is_source", type: "INTEGER", nullable: false),
                    isupdatestock = table.Column<bool>(name: "is_update_stock", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stockprocessdetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_stockprocessdetail_stockprocess_stock_process_id",
                        column: x => x.stockprocessid,
                        principalTable: "stockprocess",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dispatchpicklist_dispatchlist_id",
                table: "dispatchpicklist",
                column: "dispatchlist_id");

            migrationBuilder.CreateIndex(
                name: "IX_sku_spu_id",
                table: "sku",
                column: "spu_id");

            migrationBuilder.CreateIndex(
                name: "IX_stockprocessdetail_stock_process_id",
                table: "stockprocessdetail",
                column: "stock_process_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asn");

            migrationBuilder.DropTable(
                name: "asnsort");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "dispatchpicklist");

            migrationBuilder.DropTable(
                name: "freightfee");

            migrationBuilder.DropTable(
                name: "goodslocation");

            migrationBuilder.DropTable(
                name: "goodsowner");

            migrationBuilder.DropTable(
                name: "menu");

            migrationBuilder.DropTable(
                name: "rolemenu");

            migrationBuilder.DropTable(
                name: "sku");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "stockadjust");

            migrationBuilder.DropTable(
                name: "stockfreeze");

            migrationBuilder.DropTable(
                name: "stockmove");

            migrationBuilder.DropTable(
                name: "stockprocessdetail");

            migrationBuilder.DropTable(
                name: "stocktaking");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "userrole");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.DropTable(
                name: "warehousearea");

            migrationBuilder.DropTable(
                name: "dispatchlist");

            migrationBuilder.DropTable(
                name: "spu");

            migrationBuilder.DropTable(
                name: "stockprocess");
        }
    }
}
