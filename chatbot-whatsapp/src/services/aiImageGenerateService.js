require('dotenv').config();
const imageDalleGrpcService = require('../grpc/services/imageDalleGrpcService');

async function generacheckIfMessageRequestsAIImageGenerate(message, numberPhone) {
  try{
    if (message.startsWith("!IA-imagem") && numberPhone === process.env.NUMBER_PHONE) {
      var aiImage = await GenerateImageAI(message);
      return { succes: true, url: aiImage };
    }
    if(message.startsWith("!IA-imagem")) {
      return { succes: false, message: "Você não tem autorização para gerar imagens" };
    }

    return null;
  } catch (error){
    return { succes: false, message: error };
  }
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
        return;
      }
      resolve(response.url);
    });
  });
}

module.exports = {
  generacheckIfMessageRequestsAIImageGenerate
}