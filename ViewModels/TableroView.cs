using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EspacioTablero;
namespace tl2_tp10_2023_vaninaze.ViewModels
{
    public class TableroView{
        public int Id {get;set;}
        public string UsuarioPropietario {get;set;}
        public string Nombre {get;set;}
        public string Descripcion {get;set;}
        
        public TableroView()
        {
        }
        public TableroView(Tablero tablero, Usuario usuario)
        {  
            Id = tablero.Id;
            UsuarioPropietario = usuario.Nombre_de_usuario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }   
    } 
}