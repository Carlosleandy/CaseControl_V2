import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'sweetalert2/src/sweetalert2.scss'

interface SystemMessageOptions {
  type: 'success' | 'error' | 'warning' | 'info';
  description: string;
}

export function systemMessage(options: SystemMessageOptions): void {
  const { type, description } = options;
  
  Swal.fire({
    title: description,
    icon: type,
    color: "",
    background: "",
    customClass: {
      confirmButton: 'severity="primary', 
      popup: 'animated zoomIn' 
    },
    confirmButtonText: "Aceptar",
    confirmButtonColor: "#3085d6",
    buttonsStyling: true
  });
}
