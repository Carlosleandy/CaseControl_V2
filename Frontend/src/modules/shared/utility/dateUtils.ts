/**
 * Formatea una fecha en formato ISO (2025-04-19T17:49:33.7099667) a formato de solo fecha (2025-04-19)
 * @param dateString Cadena de fecha en formato ISO
 * @returns Cadena de fecha en formato YYYY-MM-DD
 */
export function formatDate(dateString: string): string {
  if (!dateString) return '';
  return dateString.split('T')[0];
}

/**
 * Formatea una fecha en formato ISO a un formato personalizado
 * @param dateString Cadena de fecha en formato ISO
 * @param format Formato deseado (por implementar si se necesitan más formatos)
 * @returns Cadena de fecha formateada
 */
export function formatDateCustom(dateString: string, format: string = 'YYYY-MM-DD'): string {
  if (!dateString) return '';
  
  // Por ahora solo soportamos el formato YYYY-MM-DD
  // Se puede expandir para soportar más formatos si es necesario
  if (format === 'YYYY-MM-DD') {
    return dateString.split('T')[0];
  }
  
  return dateString;
}
