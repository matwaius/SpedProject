export default function EnsureIsAuthenticated({ next, router }) {
    const data = JSON.parse(localStorage.getItem('user'));
    if (data === null || !data.Login) {
        return router.push({ name: 'Signin' });
    }
    return next();
}