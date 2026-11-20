// Prisma returneaza campurile Decimal ca obiecte care in JSON devin text ("250").
// Le convertim in numere, ca frontend-ul sa primeasca 250 si nu "250".
export function toNumber(value) {
  return value === null || value === undefined ? value : Number(value);
}
