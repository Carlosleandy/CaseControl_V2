import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'sweetalert2/src/sweetalert2.scss'
//import 'bootstrap/dist/css/bootstrap.css';
import axios from "axios";

export function showAlert(mensaje,icono,foco='')  {
    if(foco !==''){
        document.getElementById(foco).focus();
    }
    Swal.fire({
        title:mensaje,
        icon:icono,
        color: "",
        background: "",
        customClass: {
            confirmButton: 'severity="primary', 
            popup:'animated zoomIn' 
        },
        confirmButtonText: "Aceptar",
        confirmButtonColor: "#3085d6",
        buttonsStyling:true
    });
}
