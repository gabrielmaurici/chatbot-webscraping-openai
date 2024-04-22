const imageDalleGrpcService = require('../grpc/services/imageDalleGrpcService');

async function generacheckIfMessageRequestsAIImageGenerate(message) {
    if (message.startsWith("!IA-imagem")) {
        var aiImage = await GenerateImageAI(message);
        return aiImage;
    }
    return null;
}

async function GenerateImageAI(imageDescription) {
    const client = await imageDalleGrpcService.getClientGrpc();
    return new Promise((resolve, reject) => {
      const request = {
        imageDescription: imageDescription 
      };
      client.GenerateImage(request, (error, response) => {
        if (error) {
          reject("Ocorreu algum erro ao tentar falar com a IA: " + error);
        }
        var aiImage = {
            revisedPrompt: response.revisedPrompt,
            base64: response.base64
        }
        resolve(aiImage);
      });
    });
}

module.exports = {
    generacheckIfMessageRequestsAIImageGenerate
  }