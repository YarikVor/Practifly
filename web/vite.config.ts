import {defineConfig} from 'vite'
import react from '@vitejs/plugin-react'
import eslint from "@rollup/plugin-eslint";

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        react(),
        {
            ...eslint(
                {
                    include: 'src/**/*.+(js|jxs|ts|tsx)'
                },
            ),
            enforce: 'pre',
        },
    ],
    server: {
        host: true,
        port: 3030
    },
    preview: {
        port: 3030
    }
})
