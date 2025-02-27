namespace FSI.ProcedureGenerator.Domain.Entity
{
    public class BoardedPassengerReport : BaseEntity
    {
        public int AmsRecordId { get; set; } // Identificador do Registro AMS
        public int ReportNumber { get; set; } // 1 Número do RPE
        public string ReferenceAirport { get; set; } = string.Empty;// 2 Aeroporto de Referência
        public string AircraftRegistration { get; set; } = string.Empty;// 3 Matrícula da Aeronave
        public string OperatorCode { get; set; } = string.Empty;// 4 Código da Empresa Aérea
        public string FlightNumberDeparture { get; set; } = string.Empty;// 5 Número do Voo de Decolagem
        public string ScheduledDepartureDate { get; set; } = string.Empty;// 6 Data Programada da Decolagem
        public string ScheduledDepartureTime { get; set; } = string.Empty;// 7 Horário Programado da Decolagem
        public string FlightNatureDeparture { get; set; } = string.Empty;// 8 Natureza do Voo na Decolagem
        public string FlightSpecies { get; set; } = string.Empty;// 9 Tipo do Voo
        public string OperatorRole { get; set; } = string.Empty;// 10 Participação da Companhia Aérea
        public int BoardingFeeDomestic { get; set; } // Passageiros Tarifados no Embarque Doméstico
        public int BoardingFeeInternational { get; set; } // Passageiros Tarifados no Embarque Internacional
        public int InfantPassengersDeparture { get; set; } // Passageiros de até 2 anos embarcados
        public int TransitPassengers { get; set; } // Passageiros em Trânsito (Escala)
        public int ConnectionFeeDomestic { get; set; } // Passageiros Tarifados em Conexão Doméstica
        public int ConnectionFeeInternational { get; set; } // Passageiros Tarifados em Conexão Internacional
        public int CheckedBaggageDomestic { get; set; } // Peso das Bagagens Despachadas em Voos Domésticos (kg)
        public int CheckedBaggageInternational { get; set; } // Peso das Bagagens Despachadas em Voos Internacionais (kg)
        public int CargoWeightDomestic { get; set; } // Peso da Carga Embarcada em Voos Domésticos (kg)
        public int CargoWeightInternational { get; set; } // Peso da Carga Embarcada em Voos Internacionais (kg)
        public int MailWeightDomestic { get; set; } // Peso do Correio Embarcado em Voos Domésticos (kg)
        public int MailWeightInternational { get; set; } // Peso do Correio Embarcado em Voos Internacionais (kg)
        public string FlightNumberArrival { get; set; } = string.Empty;// Número do Voo de Pouso
        public string ScheduledArrivalDate { get; set; } = string.Empty;// Data Programada do Pouso
        public string ScheduledArrivalTime { get; set; } = string.Empty;// Horário Programado do Pouso
        public string FlightNatureArrival { get; set; } = string.Empty;// Natureza do Voo no Pouso
        public int PassengersFinalDestination { get; set; } // Passageiros que Finalizaram a Viagem no Aeroporto
        public int ConnectingPassengersDomestic { get; set; } // Passageiros em Conexão Doméstica no Pouso
        public int ConnectingPassengersInternational { get; set; } // Passageiros em Conexão Internacional no Pouso
        public int CheckedBaggageArrivalDomestic { get; set; } // Peso das Bagagens Desembarcadas (Doméstico)
        public int CheckedBaggageArrivalInternational { get; set; } // Peso das Bagagens Desembarcadas (Internacional)
        public int CargoWeightArrivalDomestic { get; set; } // Peso da Carga Desembarcada (Doméstico)
        public int CargoWeightArrivalInternational { get; set; } // Peso da Carga Desembarcada (Internacional)
        public int MailWeightArrivalDomestic { get; set; } // Peso do Correio Desembarcado (Doméstico)
        public int MailWeightArrivalInternational { get; set; } // Peso do Correio Desembarcado (Internacional)
        public string FlightTypeArrival { get; set; } = string.Empty;// Código do Tipo de Voo na Chegada
        public string FlightTypeDeparture { get; set; } = string.Empty;// Código do Tipo de Voo na Decolagem
        public string PreviousAirport { get; set; } = string.Empty;// Aeroporto Anterior ao Pouso
        public string NextAirport { get; set; } = string.Empty;// Aeroporto Seguinte à Decolagem
        public string AircraftType { get; set; } = string.Empty;// Tipo da Aeronave
        public string OriginFileRead { get; set; } = string.Empty;// Origem do arquivo lido
        public string TypeOfAction { get; set; } = string.Empty;// Tipo de Ação
        public bool WasItSentToAnac { get; set; } //Foi enviado para anac?
    }
}
