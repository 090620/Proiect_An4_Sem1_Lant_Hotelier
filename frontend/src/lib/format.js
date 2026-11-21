const priceFormat = new Intl.NumberFormat('ro-RO', { style: 'currency', currency: 'RON' });

export function formatPrice(value) {
  return priceFormat.format(value);
}
