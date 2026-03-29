namespace edu_connect_backend.Model
{
    public enum StatusMatricula
    {
        Iniciado = 0,               // Apenas preencheu passo 1 (nome/email), aguardando OTP
        AguardandoDados = 1,        // OTP validado, preenchendo endereço/série
        AguardandoAnaliseDocs = 2,  // Enviou tudo, coordenador precisa ver
        AguardandoPagamento = 3,    // Coordenador aprovou docs, falta pix
        Finalizado = 4,             // Tudo certo, aluno criado
        Rejeitado = 5               // Coordenador rejeitou algo
    }

    public enum TipoDocumentoMatricula
    {
        Identidade = 0,
        HistoricoEscolar = 1,
        ComprovanteResidencia = 2,
        Foto3x4 = 3,
        ComprovantePagamento = 4
    }

    public enum Turno
    {
        Manha,
        Tarde,
        Noite
    }
}