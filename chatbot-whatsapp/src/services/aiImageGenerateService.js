require('dotenv').config();
const imageDalleGrpcService = require('../grpc/services/imageDalleGrpcService');

async function generacheckIfMessageRequestsAIImageGenerate(message, numberPhone) {
    console.log("meu numero " +process.env.NUMBER_PHONE);
    if (message.startsWith("!IA-imagem") && numberPhone === process.env.NUMBER_PHONE) {
      var aiImage = await GenerateImageAI(message);
      return { authorized: true, url: aiImage };
    }
    if(message.startsWith("!IA-imagem")) {
      return { authorized: false };
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
        reject("Ocorreu algum erro ao tentar gerar imagem com a IA: " + error);
      }
      resolve(response.url);
    });
  });
}

module.exports = {
  generacheckIfMessageRequestsAIImageGenerate
}