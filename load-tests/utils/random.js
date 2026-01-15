export function randomIntBetween(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

export function randomDate(start, end) {
    const date = new Date(
        start.getTime() + Math.random() * (end.getTime() - start.getTime())
    );

    return date.toISOString().split('T')[0]; // yyyy-MM-dd
}
