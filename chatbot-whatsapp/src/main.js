const { Client, LocalAuth, MessageMedia } = require('whatsapp-web.js');
const qrcode = require('qrcode-terminal');
const botMenuService = require('./services/botMenuService');
const lastMatchService = require('./services/lastMatchService');
const nextMatchService = require('./services/nextMatchService');
const chatGptService = require('./services/chatGptService');
const aiImageGenerateService = require('./services/aiImageGenerateService');

const client = new Client({
    authStrategy: new LocalAuth(),
    restartOnAuthFail: true,
    puppeteer: {
        headless: true,
        args: ['--no-sandbox', '--disable-setuid-sandbox']
    }
});

client.once('ready', async () => {
    const version = await client.getWWebVersion();
    console.log(`WebVersion: ${version}\nClient is ready!`);
});

client.on('qr', (qr) => {
    qrcode.generate(qr, {small: true});
});

client.on('message_create', async (message) => {
    const botMenuMessage = botMenuService.checkIfMessageRequestsBotMenu(message.body);
    if(botMenuMessage){
        message.reply(botMenuMessage);
    }
    
    const lastMatchMessage = await lastMatchService.checkIfMessageRequestsLastMatch(message.body);
    if(lastMatchMessage){
        message.reply(lastMatchMessage);
    }

    const nextMatchMessage = await nextMatchService.checkIfMessageRequestsNextMatch(message.body);
    if(nextMatchMessage){
        message.reply(nextMatchMessage)
    }

    const askQuestionIAMessage = await chatGptService.checkIfMessageRequestsAskQuestionsIA(message.body);
    if(askQuestionIAMessage){
        message.reply(askQuestionIAMessage)
    }

    const aiImageGenerateMessage = await aiImageGenerateService.generacheckIfMessageRequestsAIImageGenerate(message.body);
    if(aiImageGenerateMessage){
        if(!aiImageGenerateMessage.succes){
            message.reply(aiImageGenerateMessage.message)
            return;
        }

        const optionsMedia = {
            unsafeMime: true
        }
        const media = await MessageMedia.fromUrl(aiImageGenerateMessage.url, optionsMedia);
        await message.reply(media);
    }
});

client.initialize();