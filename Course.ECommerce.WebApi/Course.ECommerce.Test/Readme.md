#AGREGAR MIGRACION
add-migration Inicial -Context ECommerceDbContext

#APLICAR MIGRACION
Update-DataBase -Context ECommerceDbContext
#Quitar MIGRATION
remove-migration //quita la ultima migracion
# Realizar migracion por script
Script-Migration -Context ECommerceDbContext -From Inicial
# Genera script desde la primera migracion hasta la ultima
Script-Migration -Context ECommerceDbContext 0

