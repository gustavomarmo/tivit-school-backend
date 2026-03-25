namespace edu_connect_backend.Infrastructure.AI
{
    public class AiService
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public AiService(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<string> GerarExercicios(string textoConteudo, string nomeMaterial)
        {
            var apiKey = configuration["Ai:ApiKey"]
                ?? throw new InvalidOperationException("Chave da API Groq não configurada.");

            var payload = new
            {
                model = "llama-3.3-70b-versatile",
                temperature = 0.4,
                max_tokens = 1800,
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "Você é um professor experiente. Leia o conteúdo do material abaixo e crie exercícios de múltipla escolha de alta qualidade. Responda APENAS com as questões, sem introduções ou despedidas."
                    },
                    new
                    {
                        role = "user",
                        content = $@"Com base no conteúdo abaixo, crie exatamente 5 questões de múltipla escolha (a, b, c, d).

Formato OBRIGATÓRIO:

Questão 1: <enunciado>
a) <alternativa>
b) <alternativa>
c) <alternativa>
d) <alternativa>
Resposta: <letra correta>

Questão 2: ...

Conteúdo do material:
{textoConteudo}"
                    }
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.groq.com/openai/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {apiKey}");
            request.Content = JsonContent.Create(payload);

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new InvalidOperationException($"Erro na API Groq: {erro}");
            }

            var resultado = await response.Content.ReadFromJsonAsync<AiApiResponse>()
                ?? throw new InvalidOperationException("Resposta inválida da API Groq.");

            return resultado.Choices[0].Message.Content;
        }
    }

    internal class AiApiResponse
    {
        public List<AiChoice> Choices { get; set; } = new();
    }

    internal class AiChoice
    {
        public AiMessage Message { get; set; } = new();
    }

    internal class AiMessage
    {
        public string Content { get; set; } = string.Empty;
    }
}