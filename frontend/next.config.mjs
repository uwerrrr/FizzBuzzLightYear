/** @type {import('next').NextConfig} */
const nextConfig = {
  output: "standalone",
  // This sets base path for your API requests when running in Docker
  env: {
    NEXT_PUBLIC_API_URL: process.env.NEXT_PUBLIC_API_URL,
  },
};

export default nextConfig;
