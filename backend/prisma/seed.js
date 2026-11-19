import { PrismaClient } from '@prisma/client';
import bcrypt from 'bcryptjs';

const prisma = new PrismaClient();

// Date implicite DOAR pentru dezvoltare locala. In productie se seteaza din variabile de mediu.
const ADMIN_EMAIL = process.env.ADMIN_EMAIL || 'admin@hotelexplorer.ro';
const ADMIN_PASSWORD = process.env.ADMIN_PASSWORD || 'Admin123!';
const DEMO_EMAIL = 'user@hotelexplorer.ro';
const DEMO_PASSWORD = 'User123!';

async function seedUsers() {
  const accounts = [
    { email: ADMIN_EMAIL, password: ADMIN_PASSWORD, fullName: 'Administrator', role: 'ADMIN' },
    { email: DEMO_EMAIL, password: DEMO_PASSWORD, fullName: 'Utilizator Demo', role: 'USER' },
  ];

  for (const { email, password, fullName, role } of accounts) {
    const passwordHash = await bcrypt.hash(password, 10);
    await prisma.user.upsert({
      where: { email },
      update: {},
      create: { email, passwordHash, fullName, role },
    });
  }
}

async function seedHotels() {
  if ((await prisma.chain.count()) > 0) {
    console.log('Lanturile exista deja, sar peste datele de exemplu.');
    return;
  }

  const carpathia = await prisma.chain.create({
    data: {
      name: 'Carpathia Hotels',
      description: 'Lant de hoteluri din zona montana a Romaniei.',
    },
  });
  const danube = await prisma.chain.create({
    data: {
      name: 'Danube Resorts',
      description: 'Hoteluri si resorturi pe malul Dunarii si in Delta.',
    },
  });

  const hotelsData = [
    {
      chainId: carpathia.id,
      name: 'Hotel Brasov Central',
      city: 'Brasov',
      country: 'Romania',
      address: 'Strada Republicii 12',
      description: 'Hotel in centrul istoric, la 5 minute de Piata Sfatului.',
    },
    {
      chainId: carpathia.id,
      name: 'Sinaia Palace',
      city: 'Sinaia',
      country: 'Romania',
      address: 'Bulevardul Carol I 40',
      description: 'Hotel de lux cu vedere spre Bucegi.',
    },
    {
      chainId: danube.id,
      name: 'Delta Blue Resort',
      city: 'Tulcea',
      country: 'Romania',
      address: 'Strada Portului 3',
      description: 'Resort pe malul Dunarii, punct de plecare spre Delta.',
    },
  ];

  const hotels = [];
  for (const data of hotelsData) {
    hotels.push(await prisma.hotel.create({ data }));
  }

  const roomTypes = [
    { type: 'Single', price: 250, description: 'Camera pentru o persoana.' },
    { type: 'Dubla', price: 380, description: 'Camera dubla cu pat matrimonial.' },
    { type: 'Suita', price: 650, description: 'Suita cu living si vedere panoramica.' },
  ];
  for (const hotel of hotels) {
    await prisma.room.createMany({
      data: roomTypes.map((room) => ({ ...room, hotelId: hotel.id })),
    });
  }

  const [brasov, sinaia, tulcea] = hotels;
  const services = [
    {
      chainId: carpathia.id,
      name: 'Wi-Fi gratuit',
      icon: 'wifi',
      hotelIds: [brasov.id, sinaia.id],
    },
    { chainId: carpathia.id, name: 'Mic dejun', price: 60, icon: 'coffee', hotelIds: [brasov.id] },
    { chainId: carpathia.id, name: 'Spa', price: 120, icon: 'spa', hotelIds: [sinaia.id] },
    { chainId: danube.id, name: 'Parcare', price: 30, icon: 'car', hotelIds: [tulcea.id] },
    {
      chainId: danube.id,
      name: 'Excursii in Delta',
      price: 200,
      icon: 'boat',
      hotelIds: [tulcea.id],
    },
  ];
  for (const { hotelIds, ...service } of services) {
    await prisma.service.create({
      data: { ...service, hotels: { connect: hotelIds.map((id) => ({ id })) } },
    });
  }

  await prisma.offer.create({
    data: {
      chainId: carpathia.id,
      title: 'Reducere de iarna',
      description: 'Sejur la munte cu 15% reducere.',
      oldPrice: 400,
      newPrice: 340,
      discountPercent: 15,
      promoCode: 'WINTER15',
    },
  });
  await prisma.offer.create({
    data: {
      chainId: danube.id,
      title: 'Weekend in Delta',
      description: 'Doua nopti cu 20% reducere.',
      oldPrice: 760,
      newPrice: 608,
      discountPercent: 20,
      promoCode: 'DELTA20',
    },
  });

  await prisma.event.create({
    data: {
      chainId: carpathia.id,
      title: 'Revelion la munte',
      description: 'Petrecere de Revelion cu program artistic.',
      startsAt: new Date('2026-12-31T20:00:00Z'),
      location: 'Sinaia Palace',
    },
  });
}

async function main() {
  await seedUsers();
  await seedHotels();
  console.log('Seed finalizat.');
}

main()
  .catch((error) => {
    console.error(error);
    process.exitCode = 1;
  })
  .finally(() => prisma.$disconnect());
