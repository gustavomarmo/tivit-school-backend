namespace edu_connect_backend.Domain.Enums
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
}
