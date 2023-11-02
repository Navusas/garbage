# Spin demo

## Prereq
```bash
# Install
curl -fsSL https://developer.fermyon.com/downloads/install.sh | bash
sudo mv ./spin /usr/local/bin/spin

```

## Add support for dotnet SDK
- [Install Rust](https://www.rust-lang.org/tools/install), which is required for [Wizer](https://github.com/bytecodealliance/wizer), which is required for [Spin dotnet SDK](https://github.com/fermyon/spin-dotnet-sdk)
   ```bash
   # Rust
   curl --proto '=https' --tlsv1.2 -sSf https://sh.rustup.rs | sh

   # Wizer
   cargo install wizer --all-features

   # Install dotnet template
   spin templates install --git https://github.com/fermyon/spin-dotnet-sdk --branch main --update
   ``````

## Create new project
```bash
spin new 

# choose http-csharp

```