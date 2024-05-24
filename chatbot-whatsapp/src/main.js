require('dotenv').config();
const { Client, RemoteAuth, LocalAuth, MessageMedia } = require('whatsapp-web.js');
const { MongoStore } = require('wwebjs-mongo');
const mongoose = require('mongoose');
const qrcode = require('qrcode-terminal');
const botMenuService = require('./services/botMenuService');
const lastMatchService = require('./services/lastMatchService');
const nextMatchService = require('./services/nextMatchService');
const chatGptService = require('./services/chatGptService');
const aiImageGenerateService = require('./services/aiImageGenerateService');

const mongoDbUrl = process.env.MONGODB_URL;
console.log(mongoDbUrl);
mongoose.connect(mongoDbUrl).then(() => {
    const store = new MongoStore({ mongoose: mongoose });
    const client = new Client({
        authStrategy: new RemoteAuth({
            store: store,
            backupSyncIntervalMs: 300000
        }),
        webVersionCache: {
            type: 'remote',
            remotePath: 'https://raw.githubusercontent.com/wppconnect-team/wa-version/main/html/2.2409.0.html',
        },
        puppeteer: {
            headless: true,
            args: ['--no-sandbox', '--disable-setuid-sandbox']
        }
    });

    client.once('ready', () => {
        console.log('Client is ready!');
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
});




// const client = new Client({
//     authStrategy: new LocalAuth(),
//     webVersionCache: {
//         type: 'remote',
//         remotePath: 'https://raw.githubusercontent.com/wppconnect-team/wa-version/main/html/2.2409.0.html',
//     },
//     puppeteer: {
//         headless: true,
//         args: ['--no-sandbox', '--disable-setuid-sandbox']
//     }
// });

