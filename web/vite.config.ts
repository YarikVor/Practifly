import {defineConfig} from "vite";
import reactRefresh from "@vitejs/plugin-react-refresh";
import {visualizer} from "rollup-plugin-visualizer";
import eslint from "@rollup/plugin-eslint";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    react(),
    {
      ...eslint(
        {
          include: "src/**/*.+(js|jxs|ts|tsx)",
        },
      ),
      enforce: "pre",
    },
    reactRefresh(),
    visualizer(),
  ],
  server: {
    host: true,
    port: 3030,
  },
  preview: {
    port: 3030,
  },
});
