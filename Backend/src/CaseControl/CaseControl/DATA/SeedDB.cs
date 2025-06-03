using Microsoft.EntityFrameworkCore;

namespace CaseControl.DATA
{
    public class SeedDB
    {
        private readonly DataContext _context;

        public SeedDB(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCaseStatusAsync();
            await CheckCaseTypeAsync();
            await CheckReceptionMediumAsync();
            await CheckRecommendationStatusAsync();
            await CheckRecommendationTypeAsync();
            await CheckFaultTypesAsync();
            await CheckInterviewTypesAsync();
            await CheckGerenciasAsync();
        }

        private async Task CheckGerenciasAsync()
        {
            if (!await _context.Gerencias.AnyAsync())
            {
                await _context.Gerencias.AddAsync(new Domain.Entities.Gerencia
                {
                    Name = "GERENCIA DE AUDITORIA FORENSE"
                });

                await _context.Gerencias.AddAsync(new Domain.Entities.Gerencia
                {
                    Name = "GERENCIA AUDITORIA ESPECIALIZADA"
                });

                await _context.Gerencias.AddAsync(new Domain.Entities.Gerencia
                {
                    Name = "GERENCIA AUDITORIA FORENSE Y ESPECIALIZADA"
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCaseStatusAsync()
        {
            if (!await _context.CaseStatuses.AnyAsync())
            {
                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "Registrado",
                    Percent = 0
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "Asignado al Senior",
                    Percent = 20
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "En Investigación",
                    Percent = 50
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "En espera de documentación",
                    Percent = 60
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "En espera de Empleado",
                    Percent = 70
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "Devuelto al Senior",
                    Percent = 90
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "Devualto al Gerente",
                    Percent = 95
                });

                await _context.CaseStatuses.AddAsync(new Domain.Entities.CaseStatus
                {
                    Name = "Cerrado",
                    Percent = 100
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCaseTypeAsync()
        {
            if (!await _context.CaseTypes.AnyAsync())
            {
                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Certificados Financieros con Irregularidad",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Cheques Falsos Pagados y Detectados",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Cobro Irregular de Comisiones por Tramitar Productos y Servicios",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Cuentas Abiertas para Estafa",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Denuncia Línea de Transparencia",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Diferencia Faltantes en Bóveda/Reserva Central",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Diferencias Faltantes de Cajeros Humanos",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Diferencias Faltantes en ATM's",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Diferencias Sobrantes de Cajeros Humanos",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Diferencias Sobrantes en Bóveda/Reserva Central",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Divulgación de información Confidencial / Consulta Indebida",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Esquema Piramidal o Ponzi",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Estafa con Cheques Girados de Cuentas Cerradas",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Fraude en Canales Electrónicos",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Fraudes con Tarjetas de Crédito / Débito",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Impresión Información Sensible",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Investigación de Reclamación",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Poderes Irregulares y/o falsos",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Préstamos Irregulares",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Requerimiento de Unidad Legal",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Requerimiento del Comité de Operaciones.",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Requerimiento del Subcomité de Operaciones.",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Retiros a Cuenta de Cliente Fallecido",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Retiros Irregulares por ventanilla",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Tarjetas de Débito Entregada y Enlazada a Cuentas de Terceros",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Transacciones en Cuentas de Colaboradores",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Transacciones Sospechosas",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Usurpación de Identidad",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Visitas sorpresa oficina comerciales",
                });

                await _context.CaseTypes.AddAsync(new Domain.Entities.CaseType
                {
                    Name = "Auditoría Proactiva",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckReceptionMediumAsync()
        {
            if (!await _context.ReceptionMedia.AnyAsync())
            {
                await _context.ReceptionMedia.AddAsync(new Domain.Entities.ReceptionMedium
                {
                    Name = "Comunicación",
                });

                await _context.ReceptionMedia.AddAsync(new Domain.Entities.ReceptionMedium
                {
                    Name = "Correo Electrónico",
                });

                await _context.ReceptionMedia.AddAsync(new Domain.Entities.ReceptionMedium
                {
                    Name = "Llamada Telefónica y Verbal",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRecommendationTypeAsync()
        {
            if (!await _context.RecommendationTypes.AnyAsync())
            {
                await _context.RecommendationTypes.AddAsync(new Domain.Entities.RecommendationType
                {
                    Name = "Mejora",
                });

                await _context.RecommendationTypes.AddAsync(new Domain.Entities.RecommendationType
                {
                    Name = "Acción Correctiva",
                });

                await _context.RecommendationTypes.AddAsync(new Domain.Entities.RecommendationType
                {
                    Name = "Sansión",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckRecommendationStatusAsync()
        {
            if (!await _context.RecommendationStatuses.AnyAsync())
            {
                await _context.RecommendationStatuses.AddAsync(new Domain.Entities.RecommendationStatus
                {
                    Name = "Pendiente",
                });

                await _context.RecommendationStatuses.AddAsync(new Domain.Entities.RecommendationStatus
                {
                    Name = "Implementada",
                });

                await _context.RecommendationStatuses.AddAsync(new Domain.Entities.RecommendationStatus
                {
                    Name = "Rechazada",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckFaultTypesAsync()
        {
            if (!await _context.FaultTypes.AnyAsync())
            {
                await _context.FaultTypes.AddAsync(new Domain.Entities.FaultType
                {
                    Name = "Leve",
                });

                await _context.FaultTypes.AddAsync(new Domain.Entities.FaultType
                {
                    Name = "Grave",
                });

                await _context.FaultTypes.AddAsync(new Domain.Entities.FaultType
                {
                    Name = "Muy Grave",
                });

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckInterviewTypesAsync()
        {
            if (!await _context.IntervieweeTypes.AnyAsync())
            {
                await _context.IntervieweeTypes.AddAsync(new Domain.Entities.IntervieweeType
                {
                    Name = "Empleado",
                });

                await _context.IntervieweeTypes.AddAsync(new Domain.Entities.IntervieweeType
                {
                    Name = "Cliente",
                });

                await _context.SaveChangesAsync();
            }
        }

    }
}
