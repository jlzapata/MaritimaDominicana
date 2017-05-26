# MaritimaDominicana
Proyecto Help Desk

Inicilamente, cuando arranca el programa, este se dirige al index de solicitudes ya que todos los usuarios del 
sistema, incluyendo los anonimos, pueden visualizar esta informacion y pueden visualizar las soluciones tambien.
Las demas acciones requieren que el usuario este autenticado.

En la parte izquierda hay un boton de login para loguearse al sistema. Ya hay por defecto algunos usuarios creados.
Entre ellos estan: Email: jl.zapata0@gmail.com, Password: Jz070192., Tipo de Usuario: Admin; Email: alejandro@gmail.com,
Password: Alejandro, Tipo de usuario: Member; Email: alfredo@gmail.com, Password: Alfredo, Tipo de usuario: Member.

Con estos usuarios se pueden realizar las pruebas del sistema.

Los usuarios estan clasificados por tipo de usuario por que hay acciones que no solo estan permitidas a los administradores
como la parte de la gestion de los clientes, tipos de problemas, departamentos, Lugar, etc.

Luego que el usuario esta autenticado en el sistema, ya puede realizar acciones como crear solicitudes,
modificarlas, cerrarlas, etc.

En la vista para crear las solicitudes se encontrara el formulario que pedira la informacion pertinente para crearla.
Ademas se encuentra un boton "Buscar Usuarios" con el cual el usuario puede buscar a otros usuarios para seguirlos
o dejar de seguirlos.

En la vista Index de solicitudes se encuentra un link "Detalles" por cada solicitud el cual dirigira al usuario a la vista de 
detalles en la cual se muestra la informacion de la solicitud. En esta vista tambien se encuentra el link "Gestionar"
el cual dirigira al usuario a la vista para gestionar la solicitud.

En la vista para gestionar la solicitud, se puede modificar algunas propiedades de la solicitud, ademas de poder asignarla a un usuario
y cerrarla.

Si un usuario es seguidor de otro, se mostrara una pequeña ventana emergente con los detalles principales 
de la solicitud cuando el usuario seguido inserte una nueva solicitud.

En parte de Informes->Historico de solicitudes se mostrara la informacion de las solicitudes filtradas por algunos parametros de fecha.

En Informes->Informes de volumen se muestra informacion de las solicitudes mediante graficos la cual se puede filtrar mediante ciertos parametros.

Y por ultimo, en tiempo de gestion se puede visualizar las estadisticas de la solicitud desde que se abrio hasta que se cerro incluyendo controles 
para filtar la informacion.








