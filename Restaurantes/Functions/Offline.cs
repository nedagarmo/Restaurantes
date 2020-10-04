using System;
using System.Collections.Generic;

namespace Restaurantes
{
	public class Offline
	{
		private static List<Orden> lordenes = new List<Orden>();
		private static List<ComponenteTipo> lcomponente_tipo = new List<ComponenteTipo>();
		private static List<Menu> lmenu = new List<Menu>();
		private static List<Componente> lcomponente = new List<Componente>();
		private static List<Notificacion> lnotificacion = new List<Notificacion>();

		public static List<Orden> GetOrders()
		{
			if (lordenes.Count <= 0)
			{
				Guid orden_id = Guid.NewGuid();
				Guid plato_1_id = Guid.NewGuid();
				Guid plato_2_id = Guid.NewGuid();

				lordenes.Add(new Orden()
				{
					id = orden_id,
					mesa = "1",
					fecha = DateTime.Now,
					plato_nav = new List<Plato>() {
						new Plato(){
							id = plato_1_id,
							menu = GetMenus()[0].id,
							numero = 1,
							orden = orden_id,
							orden_plato_componente_nav = new List<OrdenPlatoComponente>(){
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_1_id,
									componente = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[0].id,
									componente_nav = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[0]
								},
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_1_id,
									componente = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[1].id,
									componente_nav = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[1]
								},
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_1_id,
									componente = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[2].id,
									componente_nav = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[2]
								},
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_1_id,
									componente = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[3].id,
									componente_nav = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[3]
								},
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_1_id,
									componente = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[4].id,
									componente_nav = GetComponents(GetMenus()[0].id, GetComponentTypes()[0].id)[4]
								}
							}
						},
						new Plato(){
							id = plato_2_id,
							menu = GetMenus()[1].id,
							numero = 2,
							orden = orden_id,
							orden_plato_componente_nav = new List<OrdenPlatoComponente>(){
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_2_id,
									componente = GetComponents(GetMenus()[1].id, GetComponentTypes()[0].id)[0].id,
									componente_nav = GetComponents(GetMenus()[1].id, GetComponentTypes()[0].id)[0]
								},
								new OrdenPlatoComponente(){
									id = Guid.NewGuid(),
									plato = plato_2_id,
									componente = GetComponents(GetMenus()[1].id, GetComponentTypes()[0].id)[1].id,
									componente_nav = GetComponents(GetMenus()[1].id, GetComponentTypes()[0].id)[1]
								}
							}
						}
					}
				});
			}

			return lordenes;
		}

		public static Orden SaveOrder(Orden orden)
		{
			lordenes.Add(orden);
			return orden;
		}

		public static Orden UpdateOrder(Orden orden)
		{
			lordenes[lordenes.IndexOf(lordenes.Find(f => f.id.Equals(orden.id)))] = orden;
			return orden;
		}

		public static Usuario GetUser()
		{
			return new Usuario() { id = Guid.NewGuid(), nombre = "Nombre del usuario", 
								   perfil_nav = new Perfil() { nombre = "Mesero" }, 
								   restaurante_nav = new Restaurante() { 
										nombre = "Nombre Restaurante", 
										empresa_nav = new Empresa() { 
											nombre = "Histrión Internacional" 
										} 
								   } 
								  };
		}

		public static List<Menu> GetMenus()
		{
			if (lmenu.Count <= 0)
			{
				lmenu.Add(new Menu { id = Guid.NewGuid(), nombre = "Menu 1" });
				lmenu.Add(new Menu { id = Guid.NewGuid(), nombre = "Menu 2" });
				lmenu.Add(new Menu { id = Guid.NewGuid(), nombre = "Menu 3" });
			}

			return lmenu;
		}

		public static List<ComponenteTipo> GetComponentTypes()
		{
			if (lcomponente_tipo.Count <= 0)
			{
				lcomponente_tipo.Add(new ComponenteTipo { id = Guid.NewGuid(), nombre = "Tipo 1" });
				lcomponente_tipo.Add(new ComponenteTipo { id = Guid.NewGuid(), nombre = "Tipo 2" });
				lcomponente_tipo.Add(new ComponenteTipo { id = Guid.NewGuid(), nombre = "Tipo 3" });
			}

			return lcomponente_tipo;
		}

		public static List<Componente> GetComponents(Guid Menu, Guid TipoComponente)
		{
			if (lcomponente.Count <= 0)
			{
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 1 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 2 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 3 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 4 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 5 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
				lcomponente.Add(new Componente { id = Guid.NewGuid(), nombre = "Componente 6 - " + GetComponentTypes().Find(f => f.id.Equals(TipoComponente)).nombre, tipo = TipoComponente, menu = Menu });
			}

			return lcomponente;
		}

		public static Componente GetComponent(Guid Componente)
		{
			return lcomponente.Find(f => f.id.Equals(Componente));
		}

		public static List<Notificacion> GetNotifications()
		{
			if (lnotificacion.Count <= 0)
			{
				lnotificacion.Add(new Notificacion { id = Guid.NewGuid(), orden_nav = new Orden() { mesa = "1" } });
				lnotificacion.Add(new Notificacion { id = Guid.NewGuid(), orden_nav = new Orden() { mesa = "2" } });
				lnotificacion.Add(new Notificacion { id = Guid.NewGuid(), orden_nav = new Orden() { mesa = "3" } });
			}

			return lnotificacion;
		}

		public static Notificacion SaveNotificacion(Notificacion notificacion)
		{
			lnotificacion.Add(notificacion);
			return notificacion;
		}
	}
}

